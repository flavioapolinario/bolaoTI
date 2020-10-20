using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Enums;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;
using System.Linq;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class RankingServicoAplicacao : IRankingServicoAplicacao
    {
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;
        private readonly IRankingRepositorio _rankingRepositorio;

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ICampeonatoRepositorio _campeonatoRepositorio;
        private readonly IOrganizacaoRepositorio _organizacaoRepositorio;
        private readonly IPartidaRepositorio _partidaRepositorio;
        private readonly IApostaRepositorio _apostaRepositorio;

        public RankingServicoAplicacao(IUsuarioRepositorio usuarioRepositorio,
                                       IRankingRepositorio rankingRepositorio,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho,
                                       ICampeonatoRepositorio campeonatoRepositorio,
                                       IOrganizacaoRepositorio organizacaoRepositorio,
                                       IPartidaRepositorio partidaRepositorio,
                                       IApostaRepositorio apostaRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _rankingRepositorio = rankingRepositorio;
            _campeonatoRepositorio = campeonatoRepositorio;
            _organizacaoRepositorio = organizacaoRepositorio;
            _partidaRepositorio = partidaRepositorio;
            _apostaRepositorio = apostaRepositorio;

            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public void RealizaRanking(Campeonato campeonato)
        {
            List<Organizacao> organizacoes = _organizacaoRepositorio.FindByCampeonato(campeonato.Id).ToList();
            List<Partida> partidas = _partidaRepositorio.FindAllByCampeonato(campeonato.Id).ToList();
            bool isAtualizacao = false;

            organizacoes.ForEach(organizacao =>
            {
                organizacao.Usuarios.ForEach(usuario =>
                {
                    List<Aposta> apostas = _apostaRepositorio.FindApostasByUsuario(organizacao.Id, campeonato.Id, usuario.Id);

                    Ranking ranking = _rankingRepositorio.FindRanking(organizacao.Id, campeonato.Id, usuario.Id);
                    isAtualizacao = (ranking != null);
                    if (ranking == null)
                        ranking = new Ranking();

                    ranking.OrganizacaoId = organizacao.Id;
                    ranking.UsuarioId = usuario.Id;
                    ranking.CampeonatoId = campeonato.Id;
                    ranking.NumeroPartidas = partidas.Count();
                    ranking.NumeroApostas = apostas.Count();
                    ranking.TotalPontos = apostas.Where(a => a.PontosAposta.HasValue).Sum(a => a.PontosAposta.Value);
                    ranking.DezTotalPontos = apostas.Count(a => a.PontosAposta == (int)RegraPontuacaoEnum.AcertarPlacarExato);
                    ranking.SeteTotalPontos = apostas.Count(a => a.PontosAposta == (int)RegraPontuacaoEnum.AcertarPlacarParcial);
                    ranking.CincoTotalPontos = apostas.Count(a => a.PontosAposta == (int)RegraPontuacaoEnum.AcertarResultado);
                    ranking.DoisTotalPontos = apostas.Count(a => a.PontosAposta == (int)RegraPontuacaoEnum.AcertarResultadoParcial);

                    if (isAtualizacao)
                        _rankingRepositorio.Update(ranking);
                    else
                        _rankingRepositorio.Insert(ranking);
                });
            });
        }

        public IList<Ranking> RecuperarPorFiltro(int organizacaoId, int? campeonatoId, int? faseId, System.Guid? usuarioId)
        {
            return _rankingRepositorio.FindRankingByFilter(organizacaoId, campeonatoId, faseId, usuarioId);
        }

        private bool VerificaUsuarioMesmaColocao(List<Ranking> rankings, Ranking ranking)
        {
            return rankings.Where(r => r.UsuarioId != ranking.UsuarioId)
                             .Any(r => r.TotalPontos == ranking.TotalPontos &&
                                     r.TotalPontos == ranking.DezTotalPontos &&
                                     r.TotalPontos == ranking.SeteTotalPontos &&
                                     r.TotalPontos == ranking.CincoTotalPontos &&
                                     r.TotalPontos == ranking.DoisTotalPontos);
        }

        public void AtualizaColocaoRanking(Campeonato campeonato)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                List<Organizacao> organizacoes = _organizacaoRepositorio.FindByCampeonato(campeonato.Id).ToList();
                organizacoes.ForEach(organizacao =>
                {
                    List<Ranking> rankings = _rankingRepositorio.FindRankingGeral(organizacao.Id, campeonato.Id)
                                                                .OrderByDescending(r => r.TotalPontos)
                                                                .ThenByDescending(r => r.DezTotalPontos)
                                                                .ThenByDescending(r => r.SeteTotalPontos)
                                                                .ThenByDescending(r => r.CincoTotalPontos)
                                                                .ThenByDescending(r => r.DoisTotalPontos)
                                                                .ToList();

                    int colocacao = 1;
                    rankings.ForEach(ranking =>
                    {
                        if (VerificaUsuarioMesmaColocao(rankings, ranking))
                            ranking.Colocacao = colocacao;
                        else
                        {
                            ranking.Colocacao = colocacao;
                            colocacao++;
                        }

                        _rankingRepositorio.Update(ranking);
                    });
                });

                unidadeDeTrabalho.Completar();
            }
        }
    }
}