using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Exceptions;
using Moq;
using BolaoTI.Dominio.Interfaces.Repositorios;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class TimeServicoCadastroTeste
    {
        private TimeServicoCadastro _timeServicoCadastro;
        private Mock<ITimeRepositorio> _mockITimeRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockITimeRepositorio = new Mock<ITimeRepositorio>();
            _timeServicoCadastro = new TimeServicoCadastro(_mockITimeRepositorio.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_time_que_ja_existe_lança_exception()
        {
            // arrage
            var time = new Time() { Nome = "Brasil", NomeAbreviado = "BRA" };
            _mockITimeRepositorio
                .Setup(x => x.TimeExistente(time.Nome))
                .Returns(true);

            // act
            _timeServicoCadastro.CadastrarTime(time);
        }

        [TestMethod]
        public void Quando_cadastrar_um_time_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var time = new Time() { Nome = "Brasil", NomeAbreviado = "BRA" };
            _mockITimeRepositorio
                .Setup(x => x.TimeExistente(time.Nome))
                .Returns(false);

            // act
            _timeServicoCadastro.CadastrarTime(time);

            // assert
            _mockITimeRepositorio.Verify(x => x.Insert(time), Times.Once());
        }
    }
}
