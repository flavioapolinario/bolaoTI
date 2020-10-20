using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BolaoTI.web.BLL.Interface
{
    interface IRegraService
    {
        int CalculaPontos(Aposta aposta, Partida partida);

        BolaoTI.web.BLL.Enum.Enums.TimeVencedor VerificaTimeVencedor(int golsTimeHome, int golsTimeAway);

        bool AcertouResultadoParcial(int golsTimeHomePartida, int golsTimeAwayPartida, int golsTimeHomeAposta, int golsTimeAwayAposta);
    }
}
