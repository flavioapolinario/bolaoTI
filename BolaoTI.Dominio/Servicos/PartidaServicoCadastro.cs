using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;
namespace BolaoTI.Dominio.Servicos
{
    public class PartidaServicoCadastro : IPartidaServicoCadastro
    {
        private readonly IPartidaRepositorio _partidaRepositorio;

        public PartidaServicoCadastro(IPartidaRepositorio partidaRepositorio)
        {
            _partidaRepositorio = partidaRepositorio;
        }

        public void CadastrarPartida(Partida partida)
        {
            if (_partidaRepositorio.PartidaExistente(partida.TimeHome.Id, partida.TimeAway.Id, partida.Estadio.Id, partida.DataPartida))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Partida_Existente, partida.TimeHome.Nome, partida.TimeAway.Nome, partida.DataPartida.ToString(RegularExpression.FormatString_Date)));
            }

            _partidaRepositorio.Insert(partida);
        }
    }
}
