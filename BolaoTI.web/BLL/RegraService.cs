using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BolaoTI.web.BLL.Enum;

namespace BolaoTI.web.BLL
{
    public class RegraService : BolaoTI.web.BLL.Interface.IRegraService
    {
        public int CalculaPontos(Aposta aposta, Partida partida)
        {
            string msgRegistroNaoEncontrado = "{0} não encontrada para realizar o calculo dos pontos";

            if (aposta == null)
                throw new Exception(string.Format(msgRegistroNaoEncontrado, "Aposta"));

            if (partida == null)
                throw new Exception(string.Format(msgRegistroNaoEncontrado, "Partida"));

            if (!partida.GolsTimeHome.HasValue || !partida.GolsTimeAway.HasValue)
                return 0;

            Enums.TimeVencedor resultadoPartida = VerificaTimeVencedor(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value);
            Enums.TimeVencedor resultadoAposta = VerificaTimeVencedor(aposta.GolsTimeHome, aposta.GolsTimeAway);

            // Verifica se marcou 10 pontos - Acertar o placar exato.
            if (partida.GolsTimeHome == aposta.GolsTimeHome && partida.GolsTimeAway == aposta.GolsTimeAway)
                return 10;

            // Verifica se marcou 7 pontos - Acertar o resultado e o placar parcial.
            else if (resultadoPartida.Equals(resultadoAposta) &&
                AcertouResultadoParcial(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value,
                                        aposta.GolsTimeHome, aposta.GolsTimeAway))
                return 7;

            // Verifica se marcou 5 pontos - Acertar o resultado e errar o placar.
            else if (resultadoPartida.Equals(resultadoAposta))
                return 5;

            // Verifica se marcou 2 pontos - Errar o resultado e acertar o placar parcial   
            else if (AcertouResultadoParcial(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value,
                                        aposta.GolsTimeHome, aposta.GolsTimeAway))
                return 2;
            else
                return 0;
        }

        public BolaoTI.web.BLL.Enum.Enums.TimeVencedor VerificaTimeVencedor(int golsTimeHome, int golsTimeAway)
        {
            if (golsTimeHome > golsTimeAway)
                return BolaoTI.web.BLL.Enum.Enums.TimeVencedor.TimeHome;
            else if (golsTimeHome < golsTimeAway)
                return BolaoTI.web.BLL.Enum.Enums.TimeVencedor.TimeAway;
            else
                return BolaoTI.web.BLL.Enum.Enums.TimeVencedor.Empate;
        }

        public bool AcertouResultadoParcial(int golsTimeHomePartida, int golsTimeAwayPartida,
                                             int golsTimeHomeAposta, int golsTimeAwayAposta)
        {
            if (golsTimeHomePartida == golsTimeHomeAposta)
                return true;
            else if (golsTimeAwayPartida == golsTimeAwayAposta)
                return true;
            else
                return false;
        }
    }
}