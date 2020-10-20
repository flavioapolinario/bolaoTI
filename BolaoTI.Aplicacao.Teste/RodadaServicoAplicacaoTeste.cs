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
    public class RodadaServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IRodadaServicoCadastro> mockRodadaServicoCadastro;
        private Mock<IRodadaRepositorio> mockRodadaRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private RodadaServicoAplicacao servico;

        private Rodada GetRodadaFake()
        {
            return new Rodada() { Nome = "Rodada 1", Grupo = It.IsAny<Grupo>(), Partidas = It.IsAny<List<Partida>>(), Id = It.IsAny<int>() };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockRodadaServicoCadastro = new Mock<IRodadaServicoCadastro>();
            mockRodadaRepositorio = new Mock<IRodadaRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new RodadaServicoAplicacao(mockRodadaRepositorio.Object, mockRodadaServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Rodada = GetRodadaFake();
            mockRodadaRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Rodada);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockRodadaRepositorio.VerifyAll();
            Assert.AreSame(Rodada, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsRodadas_recuperar_todos_do_repositorio()
        {
            // arrange
            var Rodadas = new List<Rodada>();
            mockRodadaRepositorio.Setup(_ => _.FindAll()).Returns(Rodadas);

            // act
            var retorno = servico.RecuperarTodosOsRodadas();

            // assert
            mockRodadaRepositorio.VerifyAll();
            Assert.AreSame(Rodadas, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var Rodadas = new List<Rodada>();
            mockRodadaRepositorio.Setup(_ => _.FindByFilter("Rodada 1", It.IsAny<int?>(), It.IsAny<int?>())).Returns(Rodadas);

            // act
            var retorno = servico.RecuperarPorFiltro("Rodada 1", It.IsAny<int?>(), It.IsAny<int?>());

            // assert
            mockRodadaRepositorio.VerifyAll();
            Assert.AreSame(Rodadas, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarRodada_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Rodada = GetRodadaFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockRodadaServicoCadastro.Setup(_ => _.CadastrarRodada(Rodada)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarRodada(Rodada);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Rodada Rodada = GetRodadaFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockRodadaRepositorio.Setup(_ => _.Delete(Rodada)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Rodada);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockRodadaRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Rodada Rodada = GetRodadaFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockRodadaRepositorio.Setup(_ => _.Update(Rodada)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Rodada);
            }
        }
    }
}
