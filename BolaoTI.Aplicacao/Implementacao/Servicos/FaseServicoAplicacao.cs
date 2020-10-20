using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class FaseServicoAplicacao : IFaseServicoAplicacao
    {
        private readonly IFaseRepositorio _faseRepositorio;
        private readonly IFaseServicoCadastro _faseServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;
        private readonly IPartidaServicoAplicacao _partidaServicoAplicacao;
        private readonly IRankingServicoAplicacao _rankingServicoAplicacao;

        public FaseServicoAplicacao(IFaseRepositorio FaseRepositorio,
                                    IFaseServicoCadastro FaseServicoCadastro,
                                    IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho,
                                    IPartidaServicoAplicacao partidaServicoAplicacao,
                                    IRankingServicoAplicacao rankingServicoAplicacao
                                    )
        {
            _faseRepositorio = FaseRepositorio;
            _faseServicoCadastro = FaseServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
            _partidaServicoAplicacao = partidaServicoAplicacao;
            _rankingServicoAplicacao = rankingServicoAplicacao;
        }

        public virtual void CadastrarFase(Fase Fase)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _faseServicoCadastro.CadastrarFase(Fase);

                unidadeDeTrabalho.Completar();
            }
        }

        public Fase RecuperarPorId(int id)
        {
            return _faseRepositorio.Get(id);
        }

        public IList<Fase> RecuperarPorFiltro(string nome, int? idCampeonato, System.DateTime? dataReferencia)
        {
            return _faseRepositorio.FindByFilter(nome, idCampeonato, dataReferencia);
        }

        public virtual IList<Fase> RecuperarTodosOsFases()
        {
            return _faseRepositorio.FindAll();
        }

        public virtual void Remover(Fase Fase)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _faseRepositorio.Delete(Fase);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _faseRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Fase Fase)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _faseRepositorio.Update(Fase);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Fechar(Fase fase)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                fase.Grupos.ForEach(g =>
                {
                    g.Rodadas.ForEach(r =>
                    {
                        r.Partidas.ForEach(p =>
                        {
                            var partida = _partidaServicoAplicacao.RecuperarPorId(p.Id);
                            partida.GolsTimeAway = p.GolsTimeAway;
                            partida.GolsTimeHome = p.GolsTimeHome;
                            _partidaServicoAplicacao.FecharPartida(partida);
                        });
                    });
                });

                _rankingServicoAplicacao.RealizaRanking(fase.Campeonato);

                unidadeDeTrabalho.Completar();
            }
        }
    }
}
