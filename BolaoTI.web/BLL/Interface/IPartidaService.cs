using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolaoTI.web.BLL.Interface
{
    interface IPartidaService
    {
        void FecharPartida(Partida partida);
        void FecharPartida(List<Partida> partidas);
        void Save();
    }
}
