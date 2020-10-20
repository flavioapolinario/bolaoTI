using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IRodadaRepositorio : IRepositorioGenerico<Rodada>
    {
        bool RodadaExistente(string nome, int idGrupo);

        IList<Rodada> FindByFilter(string nome, int? idFase, int? idGrupo);
    }
}
