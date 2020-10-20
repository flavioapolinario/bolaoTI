using BolaoTI.Dominio;
using System.Collections.Generic;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IPartidaServicoAplicacao
    {
        void FecharPartida(Partida partida);

        void FecharPartida(IList<Partida> partidas);

        void CadastrarPartida(Partida Partida);

        Partida RecuperarPorId(int id);

        IList<Partida> RecuperarPorFiltro(int idOrganizacao, int idCampeonato, 
                                          int? idFase, int? idGrupo, int? idRodada,
                                          int? idTimeHome, int? idTimeAway, int? idEstadio);

        IList<Partida> RecuperarTodosAsPartidas();

        void Remover(Partida Partida);

        void Remover(int id);

        void Atualizar(Partida Partida);
    }
}
