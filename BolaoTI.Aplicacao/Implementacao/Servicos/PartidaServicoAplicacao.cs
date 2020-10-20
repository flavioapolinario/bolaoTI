using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class PartidaServicoAplicacao : IPartidaServicoAplicacao
    {
        private readonly IApostaServicoAplicacao _apostaServicoAplicacao;

        private readonly IPartidaRepositorio _partidaRepositorio;
        private readonly IPartidaServicoCadastro _partidaServicoCadastro;
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;


        public PartidaServicoAplicacao(IApostaServicoAplicacao apostaServicoAplicacao,
                                       IPartidaRepositorio partidaRepositorio,
                                       IPartidaServicoCadastro partidaServicoCadastro,
                                       IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {
            _apostaServicoAplicacao = apostaServicoAplicacao;
            _partidaRepositorio = partidaRepositorio;
            _partidaServicoCadastro = partidaServicoCadastro;
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        public virtual void CadastrarPartida(Partida Partida)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _partidaServicoCadastro.CadastrarPartida(Partida);

                unidadeDeTrabalho.Completar();
            }
        }

        public Partida RecuperarPorId(int id)
        {
            return _partidaRepositorio.Get(id);
        }

        public IList<Partida> RecuperarPorFiltro(int idOrganizacao, int idCampeonato,
                                                 int? idFase, int? idGrupo, int? idRodada,
                                                 int? idTimeHome, int? idTimeAway, int? idEstadio)
        {
            return _partidaRepositorio.FindByFilter(idOrganizacao, idCampeonato, idFase, idGrupo, idRodada, idTimeHome, idTimeAway, idEstadio);
        }

        public virtual IList<Partida> RecuperarTodosAsPartidas()
        {
            return _partidaRepositorio.FindAll();
        }

        public virtual void Remover(Partida Partida)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _partidaRepositorio.Delete(Partida);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Remover(int id)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _partidaRepositorio.Delete(id);

                unidadeDeTrabalho.Completar();
            }
        }

        public void Atualizar(Partida Partida)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                unidadeDeTrabalho.Iniciar();

                _partidaRepositorio.Update(Partida);

                unidadeDeTrabalho.Completar();
            }
        }

        public void FecharPartida(Partida partida)
        {
            _partidaRepositorio.Update(partida);
            _apostaServicoAplicacao.CalculaPontos(partida);
        }

        public void FecharPartida(IList<Partida> partidas)
        {
            using (var unidadeDeTrabalho = _fabricaDeUnidadeDeTrabalho.Criar())
            {
                foreach (var partida in partidas)
                {
                    FecharPartida(partida);
                }
            }
        }

    }
}
