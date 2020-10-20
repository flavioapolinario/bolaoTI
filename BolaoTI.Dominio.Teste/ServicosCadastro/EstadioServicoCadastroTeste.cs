using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class EstadioServicoCadastroTeste
    {
        private EstadioServicoCadastro _estadioServicoCadastro;
        private Mock<IEstadioRepositorio> _mockIEstadioRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIEstadioRepositorio = new Mock<IEstadioRepositorio>();
            _estadioServicoCadastro = new EstadioServicoCadastro(_mockIEstadioRepositorio.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Estadio_que_ja_existe_lança_exception()
        {
            // arrage
            var estadio = new Estadio() { Nome = "Mineirão" };
            _mockIEstadioRepositorio
                .Setup(x => x.EstadioExistente(estadio.Nome))
                .Returns(true);

            // act
            _estadioServicoCadastro.CadastrarEstadio(estadio);
        }

        [TestMethod]
        public void Quando_cadastrar_um_Estadio_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var estadio = new Estadio() { Nome = "Mineirão" };
            _mockIEstadioRepositorio
                .Setup(x => x.EstadioExistente(estadio.Nome))
                .Returns(false);

            // act
            _estadioServicoCadastro.CadastrarEstadio(estadio);

            // assert
            _mockIEstadioRepositorio.Verify(x => x.Insert(estadio), Times.Once());
        }
    }
}
