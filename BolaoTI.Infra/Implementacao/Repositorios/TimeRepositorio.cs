using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class TimeRepositorio : RepositorioGenerico<Time>, ITimeRepositorio
    {
        public TimeRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool TimeExistente(string nome)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public IList<Time> FindByFilter(string nome, string nomeAbreviado)
        {
            return _dbSet.Where(p => p.Nome.Contains(nome) || p.NomeAbreviado.Contains(nomeAbreviado)).ToList();
        }
    }
}
