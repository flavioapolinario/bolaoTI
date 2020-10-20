using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using BolaoTI.Dominio;
using System.Collections.Generic;
using Moq.Sequences;
using BolaoTI.Aplicacao.Interfaces.Servicos;

namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class PartidaServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IApostaServicoAplicacao> mockApostaServicoAplicacao;
        private Mock<IPartidaServicoCadastro> mockPartidaServicoCadastro;
        private Mock<IPartidaRepositorio> mockPartidaRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private PartidaServicoAplicacao servico;

        private Partida GetPartidaFake()
        {
            return new Partida()
            {
                Id = It.IsAny<int>(),
                DataPartida = It.IsAny<DateTime>(),
                Rodada = It.IsAny<Rodada>(),
                Estadio = It.IsAny<Estadio>(),
                TimeAway = It.IsAny<Time>(),
                TimeHome = It.IsAny<Time>(),
            };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockApostaServicoAplicacao = new Mock<IApostaServicoAplicacao>();
            mockPartidaServicoCadastro = new Mock<IPartidaServicoCadastro>();
            mockPartidaRepositorio = new Mock<IPartidaRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new PartidaServicoAplicacao(mockApostaServicoAplicacao.Object, mockPartidaRepositorio.Object, mockPartidaServicoCadastro.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Partida = GetPartidaFake();
            mockPartidaRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Partida);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockPartidaRepositorio.VerifyAll();
            Assert.AreSame(Partida, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsPartidas_recuperar_todos_do_repositorio()
        {
            // arrange
            var Partidas = new List<Partida>();
            mockPartidaRepositorio.Setup(_ => _.FindAll()).Returns(Partidas);

            // act
            var retorno = servico.RecuperarTodosAsPartidas();

            // assert
            mockPartidaRepositorio.VerifyAll();
            Assert.AreSame(Partidas, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var Partidas = new List<Partida>();
            mockPartidaRepositorio.Setup(_ => _.FindByFilter(It.IsAny<int>(), It.IsAny<int>(), null, null, null, null, null, null)).Returns(Partidas);

            // act
            var retorno = servico.RecuperarPorFiltro(It.IsAny<int>(), It.IsAny<int>(), null, null, null, null, null, null);

            // assert
            mockPartidaRepositorio.VerifyAll();
            Assert.AreSame(Partidas, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarPartida_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Partida = GetPartidaFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockPartidaServicoCadastro.Setup(_ => _.CadastrarPartida(Partida)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarPartida(Partida);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Partida Partida = GetPartidaFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockPartidaRepositorio.Setup(_ => _.Delete(Partida)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Partida);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockPartidaRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Partida Partida = GetPartidaFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockPartidaRepositorio.Setup(_ => _.Update(Partida)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Partida);
            }
        }
    }
}
