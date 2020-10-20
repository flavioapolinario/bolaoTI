using BolaoTI.Aplicacao.Interfaces.Infraestrutura;
using BolaoTI.Aplicacao.Interfaces.Servicos;
using BolaoTI.Dominio;
using BolaoTI.Dominio.Enums;
using BolaoTI.Dominio.Exceptions;
using BolaoTI.Resources;

namespace BolaoTI.Aplicacao.Implementacao.Servicos
{
    public class RegraServicoAplicacao : IRegraServicoAplicacao
    {
        private readonly IFabricaDeUnidadeDeTrabalho _fabricaDeUnidadeDeTrabalho;

        public RegraServicoAplicacao(IFabricaDeUnidadeDeTrabalho fabricaDeUnidadeDeTrabalho)
        {            
            _fabricaDeUnidadeDeTrabalho = fabricaDeUnidadeDeTrabalho;
        }

        /// <summary>
        /// Calcula a pontuação de acordo com a aposta e a partida informada
        /// </summary>
        /// <param name="aposta">Aposta realizada</param>
        /// <param name="partida">Partida disputada</param>
        /// <returns>Quantidade de pontos</returns>
        public int CalculaPontos(Aposta aposta, Partida partida)
        {
            if (aposta == null)
                throw new BolaoTIException(string.Format(Messages.ErrorMessage_CalculoPontos_SemAposta_e_Partida, Classes.Aposta_Class));

            if (partida == null)
                throw new BolaoTIException(string.Format(Messages.ErrorMessage_CalculoPontos_SemAposta_e_Partida, Classes.Partida_Class));

            if (!partida.GolsTimeHome.HasValue || !partida.GolsTimeAway.HasValue)
                return 0;

            TimeVencedorEnum resultadoPartida = VerificaTimeVencedor(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value);
            TimeVencedorEnum resultadoAposta = VerificaTimeVencedor(aposta.GolsTimeHome, aposta.GolsTimeAway);

            // Verifica se marcou 10 pontos - Acertar o placar exato.
            if (partida.GolsTimeHome == aposta.GolsTimeHome && partida.GolsTimeAway == aposta.GolsTimeAway)
                return (int)RegraPontuacaoEnum.AcertarPlacarExato;

            // Verifica se marcou 7 pontos - Acertar o resultado e o placar parcial.
            else if (resultadoPartida.Equals(resultadoAposta) &&
                AcertouResultadoParcial(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value, aposta.GolsTimeHome, aposta.GolsTimeAway))
                return (int)RegraPontuacaoEnum.AcertarPlacarParcial;

            // Verifica se marcou 5 pontos - Acertar o resultado e errar o placar.
            else if (resultadoPartida.Equals(resultadoAposta))
                return (int)RegraPontuacaoEnum.AcertarResultado;

            // Verifica se marcou 2 pontos - Errar o resultado e acertar o placar parcial   
            else if (AcertouResultadoParcial(partida.GolsTimeHome.Value, partida.GolsTimeAway.Value, aposta.GolsTimeHome, aposta.GolsTimeAway))
                return (int)RegraPontuacaoEnum.AcertarResultadoParcial;
            else
                return (int)RegraPontuacaoEnum.ErrouResultado;
        }

        /// <summary>
        /// Verifica qual o time vencedor
        /// </summary>
        /// <param name="golsTimeHome">Gols do time da Casa</param>
        /// <param name="golsTimeAway">Gols do time Visitante</param>
        /// <returns>Enum com o resultado determinado</returns>
        private TimeVencedorEnum VerificaTimeVencedor(int golsTimeHome, int golsTimeAway)
        {
            if (golsTimeHome > golsTimeAway)
                return TimeVencedorEnum.TimeHome;
            else if (golsTimeHome < golsTimeAway)
                return TimeVencedorEnum.TimeAway;
            else
                return TimeVencedorEnum.Empate;
        }

        /// <summary>
        /// Determina se o houve acerto do placar parcial
        /// </summary>
        /// <param name="golsTimeHomePartida">Gols do time da Casa, Resultado da Partida</param>
        /// <param name="golsTimeAwayPartida">Gols do time Visitante, Resultado da Partida</param>
        /// <param name="golsTimeHomeAposta">Gols do time da casa, Resultado Apostado</param>
        /// <param name="golsTimeAwayAposta">Gols do time Visitante, Resultado Apostado</param>
        /// <returns>Retorna condição se acertou ou não</returns>
        private bool AcertouResultadoParcial(int golsTimeHomePartida, int golsTimeAwayPartida, int golsTimeHomeAposta, int golsTimeAwayAposta)
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
