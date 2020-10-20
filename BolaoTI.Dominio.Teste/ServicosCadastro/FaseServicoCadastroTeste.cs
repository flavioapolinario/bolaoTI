using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Dominio.Servicos;
using BolaoTI.Dominio.Interfaces.Repositorios;
using Moq;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Dominio.Teste.ServicosCadastro
{
    [TestClass]
    public class FaseServicoCadastroTeste
    {
        private FaseServicoCadastro _faseServicoCadastro;
        private Mock<IFaseRepositorio> _mockIFaseRepositorio;

        [TestInitialize]
        public void IniciarTestes()
        {
            _mockIFaseRepositorio = new Mock<IFaseRepositorio>();
            _faseServicoCadastro = new FaseServicoCadastro(_mockIFaseRepositorio.Object);
        }

        private Fase GetFaseFake()
        {
            return new Fase()
            {
                Nome = "Fase de Grupos",
                Campeonato = new Campeonato() { Nome = "Olimpiadas 2016" },
                DataInicio = It.IsAny<DateTime>(),
                DataFim = It.IsAny<DateTime>().AddDays(7)
            };
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Quando_cadastrar_um_Fase_que_ja_existe_lança_exception()
        {
            // arrage
            Fase fase = GetFaseFake();
            _mockIFaseRepositorio
                .Setup(x => x.FaseExistente(fase.Nome, fase.Campeonato.Id))
                .Returns(true);

            // act
            _faseServicoCadastro.CadastrarFase(fase);
        }



        [TestMethod]
        public void Quando_cadastrar_um_Fase_que_não_existe_insere_no_repositorio()
        {
            // arrage
            Fase fase = GetFaseFake();
            _mockIFaseRepositorio
                .Setup(x => x.FaseExistente(fase.Nome, fase.Campeonato.Id))
                .Returns(false);

            // act
            _faseServicoCadastro.CadastrarFase(fase);

            // assert
            _mockIFaseRepositorio.Verify(x => x.Insert(fase), Times.Once());
        }
    }
}
