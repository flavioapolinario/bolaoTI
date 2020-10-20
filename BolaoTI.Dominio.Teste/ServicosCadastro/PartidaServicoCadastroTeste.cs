using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using Moq;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class PartidaServicoCadastroTeste
    {
        private PartidaServicoCadastro _partidaServicoCadastro;
        private Mock<IPartidaRepositorio> _mockIPartidaRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIPartidaRepositorio = new Mock<IPartidaRepositorio>();
            _partidaServicoCadastro = new PartidaServicoCadastro(_mockIPartidaRepositorio.Object);
        }

        private Partida GetPartidaFake()
        {
            return new Partida()
            {
                DataPartida = DateTime.Now,
                TimeHome = new Time() { Nome = "Brasil" },
                TimeAway = new Time() { Nome = "Argentina" },
                Estadio = new Estadio { Nome = "Mineirão" },
                Rodada = new Rodada { Nome = "Rodada 1" }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Partida_que_ja_existe_lança_exception()
        {
            // arrage
            Partida partida = GetPartidaFake();
            _mockIPartidaRepositorio
                .Setup(x => x.PartidaExistente(partida.TimeHome.Id, partida.TimeAway.Id, partida.Estadio.Id, partida.DataPartida))
                .Returns(true);

            // act
            _partidaServicoCadastro.CadastrarPartida(partida);
        }



        [TestMethod]
        public void Quando_cadastrar_um_Partida_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var partida = GetPartidaFake();
            _mockIPartidaRepositorio
                .Setup(x => x.PartidaExistente(partida.TimeHome.Id, partida.TimeAway.Id, partida.Estadio.Id, partida.DataPartida))
                .Returns(false);

            // act
            _partidaServicoCadastro.CadastrarPartida(partida);

            // assert
            _mockIPartidaRepositorio.Verify(x => x.Insert(partida), Times.Once());
        }
    }
}
