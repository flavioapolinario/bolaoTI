using BolaoTI.web.DAL.Interface;
using BolaoTI.web.Models;
using BolaoTI.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace BolaoTI.web.DAL
{
    public class ApostaRepository : GenericRepository<Aposta>, IApostaRepository
    {
        public ApostaRepository(BolaoTIContext context)
            : base(context)
        {
        }

        public void SalvarAposta(IEnumerable<ApostaPartidaData> apostas)
        {
            foreach (BolaoTI.web.ViewModels.ApostaPartidaData aposta in apostas)
            {
                if (aposta.GolsApostadoTimeHome.HasValue &&
                    aposta.GolsApostadoTimeAway.HasValue &&
                    aposta.IdUsuario.HasValue)
                {
                    Aposta apostaFeita = new Aposta()
                    {
                        Id = aposta.IdAposta.HasValue ? aposta.IdAposta.Value : 0,
                        GolsTimeHome = aposta.GolsApostadoTimeHome.Value,
                        GolsTimeAway = aposta.GolsApostadoTimeAway.Value,
                        Usuario = context.UserProfiles.Find(aposta.IdUsuario.Value),
                        PartidaID = aposta.IdPartida                        
                    };
                    context.Entry(apostaFeita).State = apostaFeita.Id == 0 ?
                                                  EntityState.Added :
                                                  EntityState.Modified;
                }
            }

        }

        public List<ApostaPartidaData> GetPartidasPorFase(int IdFase, int IdUsuario)
        {
            List<ApostaPartidaData> partidas = new List<ApostaPartidaData>();

            var query = (from f in context.Fases
                         join g in context.Grupos on f.Id equals g.Fase.Id
                         join r in context.Rodadas on g.Id equals r.RodadaGrupo.Id
                         join p in context.Partidas on r.Id equals p.Rodada.Id
                         join e in context.Estadios on p.EstadioJogo.Id equals e.Id
                         join th in context.Times on p.TimeHome.Id equals th.Id
                         join ta in context.Times on p.TimeAway.Id equals ta.Id
                         select new ApostaPartidaData()
                         {
                             IdFase = f.Id,
                             IdGrupo = g.Id,
                             IdRodada = r.Id,
                             IdPartida = p.Id,
                             IdTimeHome = th.Id,
                             IdTimeAway = ta.Id,
                             IdEstadio = e.Id,
                             NomeFase = f.Nome,
                             DataEncerramentoFase = f.DataEncerramento,
                             NomeGrupo = g.Nome,
                             NomeRodada = r.Nome,
                             NomeTimeHome = th.Nome,
                             NomeTimeAway = ta.Nome,
                             NomeAbreviadoTimeHome = th.NomeAbreviado,
                             NomeAbreviadoTimeAway = ta.NomeAbreviado,
                             ImageTimeHome = th.ImagemBandeira,
                             ImageTimeAway = ta.ImagemBandeira,
                             NomeEstadio = e.Nome,
                             DataPartida = p.DataPartida,
                         });

            query = query.Where(p => p.IdFase == IdFase);

            partidas = query.ToList();

            partidas.ForEach(part =>
            {                
                var usuario = (from u in context.UserProfiles
                               where u.UserId == IdUsuario
                               select u).FirstOrDefault();

                part.IdUsuario = usuario.UserId;

                var aposta = (from a in context.Apostas.Include("Usuario")
                              join p in context.Partidas on a.PartidaApostada.Id equals p.Id
                              where p.Id == part.IdPartida && a.Usuario.UserId == part.IdUsuario
                              select a).FirstOrDefault();
                if (aposta != null)
                {
                    part.NomeUsuario = usuario.UserName;
                    part.GolsApostadoTimeHome = aposta.GolsTimeHome;
                    part.GolsApostadoTimeAway = aposta.GolsTimeAway;
                    part.IdAposta = aposta.Id;
                }
            });

            return partidas;
        }
    }
}