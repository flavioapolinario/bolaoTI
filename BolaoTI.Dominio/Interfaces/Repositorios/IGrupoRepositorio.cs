using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IGrupoRepositorio : IRepositorioGenerico<Grupo>
    {
        bool GrupoExistente(string Nome, int idFase);

        IList<Grupo> FindByFilter(string nome, int? idFase);
    }
}
