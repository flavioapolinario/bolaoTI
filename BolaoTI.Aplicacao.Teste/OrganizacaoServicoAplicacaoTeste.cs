using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using BolaoTI.Dominio;
using System.Collections.Generic;
using Moq.Sequences;

namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class OrganizacaoServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IOrganizacaoServicoCadastro> mockOrganizacaoServicoCadastro;
        private Mock<IOrganizacaoRepositorio> mockOrganizacaoRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private OrganizacaoServicoAplicacao servico;

        private Organizacao GetOrganizacaoFake()
        {
            return new Organizacao()
            {
                Nome = "BolaoTI Organizacao",
                Campeonatos = It.IsAny<List<Campeonato>>(),
                Usuarios = It.IsAny<List<Usuario>>()
            };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockOrganizacaoServicoCadastro = new Mock<IOrganizacaoServicoCadastro>();
            mockOrganizacaoRepositorio = new Mock<IOrganizacaoRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new OrganizacaoServicoAplicacao(mockOrganizacaoRepositorio.Object, mockOrganizacaoServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Organizacao = GetOrganizacaoFake();
            mockOrganizacaoRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Organizacao);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockOrganizacaoRepositorio.VerifyAll();
            Assert.AreSame(Organizacao, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsOrganizacaos_recuperar_todos_do_repositorio()
        {
            // arrange
            var organizacoes = new List<Organizacao>();
            mockOrganizacaoRepositorio.Setup(_ => _.FindAll()).Returns(organizacoes);

            // act
            var retorno = servico.RecuperarTodosOrganizacoes();

            // assert
            mockOrganizacaoRepositorio.VerifyAll();
            Assert.AreSame(organizacoes, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var organizacoes = new List<Organizacao>();
            mockOrganizacaoRepositorio.Setup(_ => _.FindByFilter("BolaoTI Organizacao", It.IsAny<List<int>>(), It.IsAny<List<Guid>>())).Returns(organizacoes);

            // act
            var retorno = servico.RecuperarPorFiltro("BolaoTI Organizacao", It.IsAny<List<int>>(), It.IsAny<List<Guid>>());

            // assert
            mockOrganizacaoRepositorio.VerifyAll();
            Assert.AreSame(organizacoes, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarOrganizacao_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var organizacao = GetOrganizacaoFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockOrganizacaoServicoCadastro.Setup(_ => _.CadastrarOrganizacao(organizacao)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarOrganizacao(organizacao);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Organizacao organizacao = GetOrganizacaoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockOrganizacaoRepositorio.Setup(_ => _.Delete(organizacao)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(organizacao);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockOrganizacaoRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Organizacao organizacao = GetOrganizacaoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockOrganizacaoRepositorio.Setup(_ => _.Update(organizacao)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(organizacao);
            }
        }
    }
}
