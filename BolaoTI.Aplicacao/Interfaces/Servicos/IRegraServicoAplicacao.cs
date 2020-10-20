using BolaoTI.Dominio;
using BolaoTI.Dominio.Enums;

namespace BolaoTI.Aplicacao.Interfaces.Servicos
{
    public interface IRegraServicoAplicacao
    {
        int CalculaPontos(Aposta aposta, Partida partida);
    }
}
