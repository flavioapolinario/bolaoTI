using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface ICampeonatoRepositorio : IRepositorioGenerico<Campeonato>
    {
        bool CampeonatoExistente(string nome, System.DateTime Inicio, System.DateTime Fim);

        IList<Campeonato> FindByFilter(string nome, System.DateTime? Inicio, System.DateTime? Fim);

        IList<Campeonato> FindByOrganizacao(int idOrganizacao);
    }
}
