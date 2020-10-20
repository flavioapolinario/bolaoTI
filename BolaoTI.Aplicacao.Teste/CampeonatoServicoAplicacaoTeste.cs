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
    public class CampeonatoServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<ICampeonatoServicoCadastro> mockCampeonatoServicoCadastro;
        private Mock<ICampeonatoRepositorio> mockCampeonatoRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private CampeonatoServicoAplicacao servico;

        private Campeonato GetCampeonatoFake()
        {
            return new Campeonato() { Id = It.IsAny<int>(), Nome = "Mineirao", NomeAbreviado = "OLI2016", Inicio = DateTime.Now, Fim = DateTime.Now, Fases = It.IsAny<List<Fase>>() };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockCampeonatoServicoCadastro = new Mock<ICampeonatoServicoCadastro>();
            mockCampeonatoRepositorio = new Mock<ICampeonatoRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new CampeonatoServicoAplicacao(mockCampeonatoRepositorio.Object, mockCampeonatoServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Campeonato = GetCampeonatoFake();
            mockCampeonatoRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Campeonato);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockCampeonatoRepositorio.VerifyAll();
            Assert.AreSame(Campeonato, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsCampeonatos_recuperar_todos_do_repositorio()
        {
            // arrange
            var Campeonatos = new List<Campeonato>();
            mockCampeonatoRepositorio.Setup(_ => _.FindAll()).Returns(Campeonatos);

            // act
            var retorno = servico.RecuperarTodosOsCampeonatos();

            // assert
            mockCampeonatoRepositorio.VerifyAll();
            Assert.AreSame(Campeonatos, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var campeonatos = new List<Campeonato>();
            mockCampeonatoRepositorio.Setup(_ => _.FindByFilter("Olimpiadas", It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(campeonatos);

            // act
            var retorno = servico.RecuperarPorFiltro("Olimpiadas", It.IsAny<DateTime>(), It.IsAny<DateTime>());

            // assert
            mockCampeonatoRepositorio.VerifyAll();
            Assert.AreSame(campeonatos, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarCampeonato_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Campeonato = GetCampeonatoFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockCampeonatoServicoCadastro.Setup(_ => _.CadastrarCampeonato(Campeonato)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarCampeonato(Campeonato);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Campeonato Campeonato = GetCampeonatoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockCampeonatoRepositorio.Setup(_ => _.Delete(Campeonato)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Campeonato);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockCampeonatoRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Campeonato Campeonato = GetCampeonatoFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockCampeonatoRepositorio.Setup(_ => _.Update(Campeonato)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Campeonato);
            }
        }


        [TestMethod]
        public void Quando_RecuperarPorOrganizacao_recuperar_no_repositorio()
        {
            // arrange
            var campeonatos = new List<Campeonato>();
            mockCampeonatoRepositorio.Setup(_ => _.FindByOrganizacao(It.IsAny<int>())).Returns(campeonatos);

            // act
            var retorno = servico.RecuperarPorOrganizacao(It.IsAny<int>());

            // assert
            mockCampeonatoRepositorio.VerifyAll();
            Assert.AreSame(campeonatos, retorno);
        }
    }
}
