using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using BolaoTI.Dominio;
using System;
using System.Linq;
using System.Collections.Generic;
using BolaoTI.Resources;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class CampeonatoRepositorio : RepositorioGenerico<Campeonato>, ICampeonatoRepositorio
    {
        public CampeonatoRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool CampeonatoExistente(string nome, System.DateTime Inicio, System.DateTime Fim)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase) && (p.Inicio >= Inicio && p.Fim <= Fim));
        }

        public IList<Campeonato> FindByFilter(string nome, System.DateTime? Inicio, System.DateTime? Fim)
        {
            var query = (from c in _dbSet select c);

            if (!String.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (Inicio.HasValue)
                query = query.Where(p => p.Inicio >= Inicio);

            if (Fim.HasValue)
                query = query.Where(p => p.Fim <= Fim);

            return query.ToList();
        }

        public IList<Campeonato> FindByOrganizacao(int idOrganizacao)
        {
            var query = (from c in _dbSet.Include(Field.Campeonato_Organizacoes_Field)
                         where c.Organizacoes.Any(o => o.Id == idOrganizacao)
                         select c);

            return query.ToList();
        }
    }
}
