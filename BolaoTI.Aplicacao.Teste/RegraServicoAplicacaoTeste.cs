using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using Moq;
using BolaoTI.Aplicacao.Implementacao.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Enums;
using System.Collections.Generic;
using BolaoTI.Dominio.Exceptions;

namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class RegraServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private RegraServicoAplicacao servico;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private Aposta _aposta = null;
        private Partida _partida = null;

        private Aposta GetApostaFake()
        {
            return new Aposta()
            {
                Id = It.IsAny<int>(),
                Usuario = It.IsAny<Usuario>(),
            };
        }

        private Partida GetPartidaFake()
        {
            return new Partida()
            {
                Id = It.IsAny<int>(),
                DataPartida = It.IsAny<DateTime>(),                
                Rodada = It.IsAny<Rodada>(),
                Estadio = It.IsAny<Estadio>(),                
                TimeAway = GetTimeAwayFake(),                
                TimeHome = GetTimeHomeFake()                
            };
        }

        private Time GetTimeHomeFake()
        {
            return new Time() { Id = 1, Nome = "Brasil", NomeAbreviado = "BRA", ImagemBandeira = @"..\\Content\themes\base\images\Bandeiras\brasil_30x30.png" };
        }

        private Time GetTimeAwayFake()
        {
            return new Time() { Id = 2, Nome = "Argentina", NomeAbreviado = "ARG", ImagemBandeira = @"..\\Content\themes\base\images\Bandeiras\argentina_30x30.png" };
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new RegraServicoAplicacao(this);

            _partida = GetPartidaFake();
            _aposta = GetApostaFake();            
            _aposta.PartidaApostada = _partida;
            _partida.Apostas = new List<Aposta>();
            _partida.Apostas.Add(_aposta);
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestMethod]
        public void Devera_CalcularPontos_Acerto_PlacarExato()
        {
            // arrange           
            // Resultado partida BRA 3 x 2 ARG
            _partida.GolsTimeHome = 3;
            _partida.GolsTimeAway = 2;

            // Resultado aspota BRA 3 x 2 ARG
            _aposta.GolsTimeHome = 3;
            _aposta.GolsTimeAway = 2;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, _partida);

            // assert            
            Assert.AreEqual((int)RegraPontuacaoEnum.AcertarPlacarExato, _aposta.PontosAposta);
        }

        [TestMethod]
        public void Devera_CalcularPontos_Acerto_PlacarParcial()
        {
            // arrange           
            // Resultado partida BRA 3 x 2 ARG
            _partida.GolsTimeHome = 3;
            _partida.GolsTimeAway = 2;

            // Resultado aspota BRA 3 x 2 ARG
            _aposta.GolsTimeHome = 4;
            _aposta.GolsTimeAway = 2;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, _partida);

            // assert            
            Assert.AreEqual((int)RegraPontuacaoEnum.AcertarPlacarParcial, _aposta.PontosAposta);
        }

        [TestMethod]
        public void Devera_CalcularPontos_Acerto_Resultado_NumeroGols_DeUmTimeSomente()
        {
            // arrange           
            // Resultado partida BRA 3 x 2 ARG
            _partida.GolsTimeHome = 3;
            _partida.GolsTimeAway = 2;

            // Resultado aspota BRA 3 x 2 ARG
            _aposta.GolsTimeHome = 2;
            _aposta.GolsTimeAway = 1;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, _partida);

            // assert            
            Assert.AreEqual((int)RegraPontuacaoEnum.AcertarResultado, _aposta.PontosAposta);
        }

        [TestMethod]
        public void Devera_CalcularPontos_Acerto_SomenteGols_DeUmTime()
        {
            // arrange           
            // Resultado partida BRA 3 x 2 ARG
            _partida.GolsTimeHome = 3;
            _partida.GolsTimeAway = 2;

            // Resultado aspota BRA 3 x 2 ARG
            _aposta.GolsTimeHome = 1;
            _aposta.GolsTimeAway = 2;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, _partida);

            // assert            
            Assert.AreEqual((int)RegraPontuacaoEnum.AcertarResultadoParcial, _aposta.PontosAposta);
        }

        [TestMethod]
        public void Devera_CalcularPontos_Errou_Resultado()
        {
            // arrange           
            // Resultado partida BRA 3 x 2 ARG
            _partida.GolsTimeHome = 3;
            _partida.GolsTimeAway = 2;

            // Resultado aspota BRA 3 x 2 ARG
            _aposta.GolsTimeHome = 0;
            _aposta.GolsTimeAway = 1;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, _partida);

            // assert            
            Assert.AreEqual((int)RegraPontuacaoEnum.ErrouResultado, _aposta.PontosAposta);
        }

        [TestMethod]
        [ExpectedException(typeof(BolaoTIException))]
        public void Devera_Lancar_Exception_Aposta_Sem_Partida()
        {
            // arrange                      
            _aposta.GolsTimeHome = 0;
            _aposta.GolsTimeAway = 1;

            // act
            _aposta.PontosAposta = servico.CalculaPontos(_aposta, null);
            
        }
    }
}
