using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IEstadioRepositorio : IRepositorioGenerico<Estadio>
    {
        bool EstadioExistente(string nome);

        IList<Estadio> FindByFilter(string nome, string cidade, string uf);
    }
}
