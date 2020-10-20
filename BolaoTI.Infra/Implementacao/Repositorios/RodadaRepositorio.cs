using BolaoTI.Resources;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class RodadaRepositorio : RepositorioGenerico<Rodada>, IRodadaRepositorio
    {
        public RodadaRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool RodadaExistente(string nome, int idGrupo)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase) && (p.Grupo.Id == idGrupo));
        }

        public IList<Rodada> FindByFilter(string nome, int? idFase, int? idGrupo)
        {
            var query = (from f in _dbSet select f);

            if (!String.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (idFase.HasValue)
                query = query.Where(p => p.Grupo.Fase.Id == idFase);

            if (idGrupo.HasValue)
                query = query.Where(p => p.Grupo.Id == idGrupo);

            return query.ToList();
        }
    }
}
