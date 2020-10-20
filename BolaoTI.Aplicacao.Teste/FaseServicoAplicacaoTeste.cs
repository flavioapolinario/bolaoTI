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
    public class FaseServicoAplicacaoTeste : IFabricaDeUnidadeDeTrabalho
    {
        private Mock<IRankingServicoAplicacao> mockRankingServicoAplicacao;
        private Mock<IPartidaServicoAplicacao> mockPartidaServicoAplicacao;
        private Mock<IFaseServicoCadastro> mockFaseServicoCadastro;
        private Mock<IFaseRepositorio> mockFaseRepositorio;
        private Mock<IUnidadeDeTrabalho> mockUnidadeDeTrabalho;
        private FaseServicoAplicacao servico;

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

        private Fase GetFaseFake()
        {
            List<Partida> partidas = new List<Partida>();
            partidas.Add(GetPartidaFake());

            List<Rodada> rodadas = new List<Rodada>();
            rodadas.Add(new Rodada()
            {
                Id = It.IsAny<int>(),
                Nome = It.IsAny<String>(),
                Ordem = It.IsAny<int>(),
                Partidas = partidas
            });

            List<Grupo> grupos = new List<Grupo>();
            grupos.Add(new Grupo()
            {
                Id = It.IsAny<int>(),
                Nome = It.IsAny<String>(),
                Rodadas = rodadas
            });

            return new Fase()
            {
                Id = It.IsAny<int>(),
                Campeonato = new Campeonato(),
                DataInicio = It.IsAny<DateTime>(),
                DataFim = It.IsAny<DateTime>().AddDays(7),
                Grupos = grupos,
                Nome = "Fase de Grupos"
            };
        }

        public IUnidadeDeTrabalho Criar()
        {
            return mockUnidadeDeTrabalho.Object;
        }

        [TestInitialize]
        public void IniciarTestes()
        {
            mockRankingServicoAplicacao = new Mock<IRankingServicoAplicacao>();
            mockPartidaServicoAplicacao = new Mock<IPartidaServicoAplicacao>();
            mockFaseServicoCadastro = new Mock<IFaseServicoCadastro>();
            mockFaseRepositorio = new Mock<IFaseRepositorio>();
            mockUnidadeDeTrabalho = new Mock<IUnidadeDeTrabalho>();

            servico = new FaseServicoAplicacao(mockFaseRepositorio.Object, mockFaseServicoCadastro.Object, this, mockPartidaServicoAplicacao.Object, mockRankingServicoAplicacao.Object);
        }

        [TestMethod]
        public void Quando_RecuperarPorID_recuperar_todos_do_repositorio()
        {
            // arrange            
            var Fase = GetFaseFake();
            mockFaseRepositorio.Setup(_ => _.Get(It.IsAny<int>())).Returns(Fase);

            // act
            var retorno = servico.RecuperarPorId(It.IsAny<int>());

            // assert
            mockFaseRepositorio.VerifyAll();
            Assert.AreSame(Fase, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarTodosOsFases_recuperar_todos_do_repositorio()
        {
            // arrange
            var Fases = new List<Fase>();
            mockFaseRepositorio.Setup(_ => _.FindAll()).Returns(Fases);

            // act
            var retorno = servico.RecuperarTodosOsFases();

            // assert
            mockFaseRepositorio.VerifyAll();
            Assert.AreSame(Fases, retorno);
        }

        [TestMethod]
        public void Quando_RecuperarPorFiltro_recuperar_no_repositorio()
        {
            // arrange
            var fases = new List<Fase>();
            mockFaseRepositorio.Setup(_ => _.FindByFilter(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<DateTime?>())).Returns(fases);

            // act
            var retorno = servico.RecuperarPorFiltro(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<DateTime?>());

            // assert
            mockFaseRepositorio.VerifyAll();
            Assert.AreSame(fases, retorno);
        }

        [TestMethod]
        public void Quando_CadastrarFase_chamar_cadastrar_do_dominio_dentro_de_uma_transacao()
        {
            // arrange
            var Fase = GetFaseFake();

            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockFaseServicoCadastro.Setup(_ => _.CadastrarFase(Fase)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.CadastrarFase(Fase);
            }
        }

        [TestMethod]
        public void Quando_Remover_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            Fase Fase = GetFaseFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockFaseRepositorio.Setup(_ => _.Delete(Fase)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(Fase);
            }
        }

        [TestMethod]
        public void Quando_RemoverPorId_chamar_Delete_do_repositorio_dentro_de_uma_transacao()
        {
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockFaseRepositorio.Setup(_ => _.Delete(It.IsAny<int>())).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Remover(It.IsAny<int>());
            }
        }

        [TestMethod]
        public void Quando_Atualizar_chamar_Update_do_repositorio_dentro_de_uma_transacao()
        {
            Fase Fase = GetFaseFake();
            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();
                mockFaseRepositorio.Setup(_ => _.Update(Fase)).InSequence();
                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Atualizar(Fase);
            }
        }

        [TestMethod]
        public void Quando_FecharFase_chamar_servicoPartida_servicoRanking_dentro_de_uma_transacao()
        {
            Fase fase = GetFaseFake();

            // arrange
            using (Sequence.Create())
            {
                mockUnidadeDeTrabalho.Setup(_ => _.Iniciar()).InSequence();

                fase.Grupos.ForEach(g =>
                {
                    g.Rodadas.ForEach(r =>
                    {
                        r.Partidas.ForEach(p =>
                        {
                            mockPartidaServicoAplicacao.Setup(_ => _.RecuperarPorId(p.Id)).InSequence().Returns(GetPartidaFake());
                            mockPartidaServicoAplicacao.Setup(_ => _.FecharPartida(p)).InSequence();
                        });
                    });
                });

                mockRankingServicoAplicacao.Setup(_ => _.RealizaRanking(fase.Campeonato)).InSequence();

                mockUnidadeDeTrabalho.Setup(_ => _.Completar()).InSequence();

                // act
                servico.Fechar(fase);
            }
        }

    }
}