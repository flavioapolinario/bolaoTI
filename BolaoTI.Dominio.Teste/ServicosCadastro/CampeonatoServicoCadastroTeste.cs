using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Moq;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class CampeonatoServicoCadastroTeste
    {
        private CampeonatoServicoCadastro _campeonatoServicoCadastro;
        private Mock<ICampeonatoRepositorio> _mockICampeonatoRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockICampeonatoRepositorio = new Mock<ICampeonatoRepositorio>();
            _campeonatoServicoCadastro = new CampeonatoServicoCadastro(_mockICampeonatoRepositorio.Object);
        }

        private Campeonato GetCampeonatoFake()
        {
            return new Campeonato() { Nome = "Olimpiadas 2016", Inicio = DateTime.Now, Fim = DateTime.Now.AddMonths(2) };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Campeonato_que_ja_existe_lança_exception()
        {
            // arrage
            var campeonato = GetCampeonatoFake();
            _mockICampeonatoRepositorio
                .Setup(x => x.CampeonatoExistente(campeonato.Nome, campeonato.Inicio, campeonato.Fim))
                .Returns(true);

            // act
            _campeonatoServicoCadastro.CadastrarCampeonato(campeonato);
        }

        [TestMethod]
        public void Quando_cadastrar_um_Campeonato_que_não_existe_insere_no_repositorio()
        {
            // arrage
            var campeonato = GetCampeonatoFake();
            _mockICampeonatoRepositorio
                .Setup(x => x.CampeonatoExistente(campeonato.Nome, campeonato.Inicio, campeonato.Fim))
                .Returns(false);

            // act
            _campeonatoServicoCadastro.CadastrarCampeonato(campeonato);

            // assert
            _mockICampeonatoRepositorio.Verify(x => x.Insert(campeonato), Times.Once());
        }
    }
}
