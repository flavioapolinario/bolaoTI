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
    public class FaseRepositorio : RepositorioGenerico<Fase>, IFaseRepositorio
    {
        public FaseRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        private string _propriedadeTimesHome = string.Format(@"{0}s.{1}s.{2}s.{3}", Classes.Grupo_Class, Classes.Rodada_Class, Classes.Partida_Class, Field.Partida_TimeHome_Field);
        private string _propriedadeTimeAway = string.Format(@"{0}s.{1}s.{2}s.{3}", Classes.Grupo_Class, Classes.Rodada_Class, Classes.Partida_Class, Field.Partida_TimeAway_Field);
        private string _propriedadeEstadio = string.Format(@"{0}s.{1}s.{2}s.{3}", Classes.Grupo_Class, Classes.Rodada_Class, Classes.Partida_Class, Field.Partida_Estadio_Field);

        public bool FaseExistente(string nome, int idCampeonato)
        {
            return _dbSet.Any(p => p.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase) && (p.Campeonato.Id == idCampeonato));
        }

        public IList<Fase> FindByFilter(string nome, int? idCampeonato, DateTime? DataReferencia)
        {
            var query = (from f in _dbSet
                         select f);

            if (!String.IsNullOrEmpty(nome))
                query = query.Where(p => p.Nome.Contains(nome));

            if (idCampeonato.HasValue)
                query = query.Where(p => p.Campeonato.Id == idCampeonato);

            if (DataReferencia.HasValue)
                query = query.Where(p => p.DataInicio >= DataReferencia.Value && p.DataFim <= DataReferencia.Value);

            return query.ToList();
        }

        public Fase FindByCampeonato(int idCampeonato, DateTime DataReferencia)
        {
            var query = (from f in _dbSet.Include(Classes.Campeonato_Class)
                                         .Include(_propriedadeTimesHome)
                                         .Include(_propriedadeTimeAway)
                                         .Include(_propriedadeEstadio)
                         select f);

            query = query.Where(p => p.Campeonato.Id == idCampeonato &&
                                     (p.DataInicio <= DataReferencia && p.DataFim >= DataReferencia));

            return query.FirstOrDefault();
        }

        public Fase FindById(int idFase)
        {
            return (from f in _dbSet.Include(Classes.Campeonato_Class)
                                         .Include(_propriedadeTimesHome)
                                         .Include(_propriedadeTimeAway)
                                         .Include(_propriedadeEstadio)
                    where f.Id == idFase
                    select f).FirstOrDefault();
        }
     
    }
}
