using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IPartidaRepositorio : IRepositorioGenerico<Partida>
    {
        bool PartidaExistente(int idTimeHome, int idTimeAway, int idEstadio, System.DateTime dataPartida);

        IList<Partida> FindByFilter(int idOrganizacao, int idCampeonato,                                    
                                    int? idFase, int? idGrupo, int? idRodada,
                                    int? idTimeHome, int? idTimeAway, int? idEstadio);

        IList<Partida> FindAllByCampeonato(int idCampeonato);

        IList<Partida> FindAllEncerradas(int idCampeonato);        
    }
}
