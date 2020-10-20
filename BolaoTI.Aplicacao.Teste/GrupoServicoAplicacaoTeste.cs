using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using Moq;
using Moq.Sequences;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio;


namespace BolaoTI.Aplicacao.Teste
{

    [TestClass]
    public class GrupoServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IGrupoServicoCadastro> mockGrupoServicoCadastro;
        private Mock<IGrupoRepositorio> mockGrupoRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private GrupoServicoAplicacao servico;

        private Grupo GetGrupoFake()
        {
            return new Grupo() { Id = It.IsAny<int>(), Nome = "Grupo A", Fase = It.IsAny<Fase>(), Rodadas = It.IsAny<List<Rodada>>() };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockGrupoServicoCadastro = new Mock<IGrupoServicoCadastro>();
            mockGrupoRepositorio = new Mock<IGrupoRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new GrupoServicoAplicacao(mockGrupoRepositorio.Object, mockGrupoServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Grupo = GetGrupoFake();
            mockGrupoRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Grupo);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockGrupoRepositorio.VerifyAll();
            Assert.AreSame(Grupo, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsGrupos_recuperar_todos_do_repositorio()
        {
            // arrange
            var Grupos = new List<Grupo>();
            mockGrupoRepositorio.Setup(_ => _.FindAll()).Returns(Grupos);

            // act
            var retorno = servico.RecuperarTodosOsGrupos();

            // assert
            mockGrupoRepositorio.VerifyAll();
            Assert.AreSame(Grupos, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var Grupos = new List<Grupo>();
            mockGrupoRepositorio.Setup(_ => _.FindByFilter("Grupo A", It.IsAny<int>())).Returns(Grupos);

            // act
            var retorno = servico.RecuperarPorFiltro("Grupo A", It.IsAny<int>());

            // assert
            mockGrupoRepositorio.VerifyAll();
            Assert.AreSame(Grupos, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarGrupo_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Grupo = GetGrupoFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockGrupoServicoCadastro.Setup(_ => _.CadastrarGrupo(Grupo)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarGrupo(Grupo);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Grupo Grupo = GetGrupoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockGrupoRepositorio.Setup(_ => _.Delete(Grupo)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Grupo);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockGrupoRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Grupo Grupo = GetGrupoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockGrupoRepositorio.Setup(_ => _.Update(Grupo)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Grupo);
            }
        }
    }
}
