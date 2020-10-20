using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class GrupoRepositorio : RepositorioGenerico<Grupo>, IGrupoRepositorio
    {
        public GrupoRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool GrupoExistente(string Nome, int idFase)
        {
            return _dbSet.Any(p => p.Nome.Equals(Nome, StringComparison.InvariantCultureIgnoreCase) && p.Fase.Id == idFase);
        }

        public System.Collections.Generic.IList<Grupo> FindByFilter(string nome, int? idFase)
        {
            var query = (from f in _dbSet select f);

            if (!String.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (idFase.HasValue)
                query = query.Where(p => p.Fase.Id == idFase);

            return query.ToList();
        }
    }
}
