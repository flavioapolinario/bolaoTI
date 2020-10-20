using BolaoTI.web.DAL.Interface;
using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BolaoTI.web.DAL
{
    public class PartidaRepository : GenericRepository<Partida>, IPartidaRepository
    {
        public PartidaRepository(BolaoTIContext context)
            : base(context)
        {
        }

        public void FecharPartidas(List<Partida> partidas)
        {
            if (partidas != null)
            {
                partidas.ForEach(this.Update);
            }
        }     
    }
}