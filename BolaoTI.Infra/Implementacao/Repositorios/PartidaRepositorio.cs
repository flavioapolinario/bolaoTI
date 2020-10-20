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
    public class PartidaRepositorio : RepositorioGenerico<Partida>, IPartidaRepositorio
    {
        public PartidaRepositorio(BolaoTIContext contexto)
            : base(contexto)
        {
        }

        public bool PartidaExistente(int idTimeHome, int idTimeAway, int idEstadio, DateTime dataPartida)
        {
            return _dbSet.Any(p => p.TimeHome.Id == idTimeHome &&
                                   p.TimeAway.Id == idTimeAway &&
                                   p.Estadio.Id == idEstadio &&
                                   p.DataPartida.ToString(BolaoTI.Resources.RegularExpression.FormatString_Date)
                                                .Equals(dataPartida.ToString(BolaoTI.Resources.RegularExpression.FormatString_Date)));
        }

        public IList<Partida> FindByFilter(int organizacaoId, int idCampeonato, int? idFase, int? idGrupo, int? idRodada, int? idTimeHome, int? idTimeAway, int? idEstadio)
        {
            string propriedades = string.Format("{0}.{1}.{2}.{3}, {4},{5}", Classes.Rodada_Class, Classes.Grupo_Class, Classes.Fase_Class, Classes.Campeonato_Class,
                                                                            Classes.Estadio_Class, Field.Partida_TimeHome_Field, Field.Partida_TimeAway_Field);

            var partidas = base.Get(filter: p =>
                (!idFase.HasValue || p.Rodada.Grupo.Fase.Id == idFase.Value) &&
                (!idGrupo.HasValue || p.Rodada.Grupo.Id == idFase.Value) &&
                (!idRodada.HasValue || p.Rodada.Id == idFase.Value) &&
                (!idTimeHome.HasValue || p.TimeHome.Id == idFase.Value) &&
                (!idTimeAway.HasValue || p.TimeAway.Id == idFase.Value) &&
                (!idEstadio.HasValue || p.Estadio.Id == idFase.Value)
            , includeProperties: propriedades);

            return partidas.ToList();
        }

        public IList<Partida> FindAllByCampeonato(int idCampeonato)
        {
            string propriedades = string.Format("{0}.{1}.{2}.{3}, {4},{5}", Classes.Rodada_Class, Classes.Grupo_Class, Classes.Fase_Class, Classes.Campeonato_Class,
                                                                            Classes.Estadio_Class, Field.Partida_TimeHome_Field, Field.Partida_TimeAway_Field);

            var apostasPartida = base.Get(filter: a => a.Rodada.Grupo.Fase.Campeonato.Id == idCampeonato, includeProperties: propriedades);

            return apostasPartida.ToList();
        }

        public IList<Partida> FindAllEncerradas(int idCampeonato)
        {
            var list = FindAllByCampeonato(idCampeonato);

            return list ?? list.Where(p => p.DataPartida < DateTime.Now).ToList();
        }
    }
}
