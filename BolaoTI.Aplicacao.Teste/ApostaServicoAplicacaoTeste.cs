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
using BolaoTI.Aplicacao.Interfaces.Servicos;

namespace BolaoTI.Aplicacao.Teste
{
    [TestClass]
    public class ApostaServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IRankingServicoAplicacao> mockRankingServicoAplicacao;
        private Mock<IRegraServicoAplicacao> mockRegraServicoAplicacao;
        private Mock<IApostaRepositorio> mockApostaRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private ApostaServicoAplicacao servico;

        private IList<Aposta> GetApostaRealizadasFake()
        {
            IList<Aposta> lista = new List<Aposta>();


            lista.Add(new Aposta()
            {
                Id = It.IsAny<int>(),
                GolsTimeAway = It.IsAny<int>(),
                GolsTimeHome = It.IsAny<int>(),
                PartidaApostada = It.IsAny<Partida>(),
                Usuario = It.IsAny<Usuario>(),
                PontosAposta = It.IsAny<int>()
            });

            return lista;
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockRankingServicoAplicacao = new Mock<IRankingServicoAplicacao>();
            mockRegraServicoAplicacao = new Mock<IRegraServicoAplicacao>();
            mockApostaRepositorio = new Mock<IApostaRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new ApostaServicoAplicacao(mockRegraServicoAplicacao.Object, mockApostaRepositorio.Object, this);
        }

        [TestMethod]
        public void Quando_RecuperarApostasPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var apostas = new List<Aposta>();
            mockApostaRepositorio.Setup(_ => _.FindApostasByFilter(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(apostas);

            // act
            var retorno = servico.RecuperarApostasPorFiltro(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid>());

            // assert
            mockApostaRepositorio.VerifyAll();
            Assert.AreSame(apostas, retorno);
        }

        [TestMethod]
        public void Quando_RealizarAposta_chamar_RealizarAposta_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var apostas = GetApostaRealizadasFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockApostaRepositorio.Setup(_ => _.RealizarAposta(apostas)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.RealizarAposta(apostas);
            }
        }

    }
}
