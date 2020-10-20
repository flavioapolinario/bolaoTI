using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Moq;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class RodadaServicoCadastroTeste
    {
        private RodadaServicoCadastro _rodadaServicoCadastro;
        private Mock<IRodadaRepositorio> _mockIRodadaRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIRodadaRepositorio = new Mock<IRodadaRepositorio>();
            _rodadaServicoCadastro = new RodadaServicoCadastro(_mockIRodadaRepositorio.Object);
        }

        private Rodada GetRodadaFake()
        {
            return new Rodada()
            {
                Nome = "Rodada 1",
                Grupo = new Grupo() { Nome = "Grupo A" }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Rodada_que_ja_existe_lança_exception()
        {
            // arrage
            Rodada rodada = GetRodadaFake();
            _mockIRodadaRepositorio
                .Setup(x => x.RodadaExistente(rodada.Nome, rodada.Grupo.Id))
                .Returns(true);

            // act
            _rodadaServicoCadastro.CadastrarRodada(rodada);
        }



        [TestMethod]
        public void Quando_cadastrar_um_Rodada_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var rodada = GetRodadaFake();
            _mockIRodadaRepositorio
                .Setup(x => x.RodadaExistente(rodada.Nome, rodada.Grupo.Id))
                .Returns(false);

            // act
            _rodadaServicoCadastro.CadastrarRodada(rodada);

            // assert
            _mockIRodadaRepositorio.Verify(x => x.Insert(rodada), Times.Once());
        }
    }
}
