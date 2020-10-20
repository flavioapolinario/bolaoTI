using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class EstadioRepositorio : RepositorioGenerico<Estadio>, IEstadioRepositorio
    {
        public EstadioRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool EstadioExistente(string nome)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public IList<Estadio> FindByFilter(string nome, string cidade, string uf)
        {
            return _dbSet.Where(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase) ||
                                     p.Cidade.Equals(cidade, StringComparison.InvariantCultureIgnoreCase) ||
                                     p.Uf.Equals(uf, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}
