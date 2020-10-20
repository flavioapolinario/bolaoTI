using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface ITimeRepositorio : IRepositorioGenerico<Time>
    {
        bool TimeExistente(string nome);

        IList<Time> FindByFilter(string nome, string nomeAbreviado);
    }
}
