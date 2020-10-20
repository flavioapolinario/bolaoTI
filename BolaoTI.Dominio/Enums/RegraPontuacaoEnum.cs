
namespace BolaoTI.Dominio.Enums
{
    public enum RegraPontuacaoEnum
    {
        /// <summary>
        /// Acertar o placar exato. 10 pontos.
        /// </summary>
        AcertarPlacarExato = 10,

        /// <summary>
        /// Acertar o resultado e o placar parcial. 7 pontos
        /// </summary>
        AcertarPlacarParcial = 7,

        /// <summary>
        ///  Acertar o resultado e errar o placar. 5 pontos
        /// </summary>
        AcertarResultado = 5,

        /// <summary>
        ///  Acertar o resultado e errar o placar. 2 pontos
        /// </summary>
        AcertarResultadoParcial = 2,
        
        /// <summary>
        ///  Errou o resultado.
        /// </summary>
        ErrouResultado = 0,
    }
}
