using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolaoTI.web.DAL.Interface
{
    interface IPartidaRepository
    {        
        void FecharPartidas(List<Partida> partidas);        
    }
}
