using BolaoTI.Dominio.Interfaces.Infraestrutura;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio.Interfaces.Repositorios
{
    public interface IFaseRepositorio : IRepositorioGenerico<Fase>
    {
        bool FaseExistente(string nome, int idCampeonato);

        IList<Fase> FindByFilter(string nome, int? idCampeonato, DateTime? DataReferencia);

        Fase FindByCampeonato(int idCampeonato, DateTime DataReferencia);

        Fase FindById(int idFase);        
    }
}
