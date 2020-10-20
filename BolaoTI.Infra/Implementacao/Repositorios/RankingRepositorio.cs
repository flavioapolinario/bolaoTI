using System.Linq;
using System.Collections.Generic;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Dominio;
using BolaoTI.Infra.ConfiguracaoEF;
using BolaoTI.Infra.Implementacao.InfraEstrutura;
using System;
using BolaoTI.Resources;

namespace BolaoTI.Infra.Implementacao.Repositorios
{
    public class RankingRepositorio : RepositorioGenerico<Ranking>, IRankingRepositorio
    {
        public RankingRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {

        }

        public List<Ranking> FindRankingByFilter(int organizacaoId, int? campeonatoId, int? faseId, Guid? usuarioId)
        {
            var query = (from r in _dbSet.Include(Classes.Campeonato_Class)
                                         .Include(Classes.Organizacao_Class)
                                         .Include(Classes.Usuario_Class)
                         where r.Organizacao.Id == organizacaoId                             
                         select r).AsQueryable();

            if (campeonatoId.HasValue)
                query = query.Where(q => q.Campeonato.Id == campeonatoId.Value);

            if (faseId.HasValue)
                query = query.Where(q => q.Campeonato.Fases.Any(f => f.Id == faseId.Value));

            if (usuarioId.HasValue)
                query = query.Where(q => q.Usuario.Id == usuarioId);

            return query.OrderBy(q => q.Colocacao).ToList();
        }

        public List<Ranking> FindRankingByFase(int organizacaoId, int campeonatoId, int? faseId)
        {
            return FindRankingByFilter(organizacaoId, campeonatoId, faseId, null);
        }

        public List<Ranking> FindRankingByUsuario(int organizacaoId, Guid? usuarioId)
        {
            return FindRankingByFilter(organizacaoId, null, null, usuarioId);
        }

        public List<Ranking> FindRankingGeral(int organizacaoId, int campeonatoId)
        {
            return FindRankingByFilter(organizacaoId, campeonatoId, null, null);
        }

        public Ranking FindRanking(int organizacaoId, int campeonatoId, Guid usuarioId)
        {
            var query = (from r in _dbSet.Include(Classes.Campeonato_Class)
                                         .Include(Classes.Organizacao_Class)
                                         .Include(Classes.Usuario_Class)
                         where r.OrganizacaoId == organizacaoId &&
                               r.CampeonatoId == campeonatoId &&
                               r.UsuarioId == usuarioId
                         select r);

            return query.FirstOrDefault();
        }
    }
}
