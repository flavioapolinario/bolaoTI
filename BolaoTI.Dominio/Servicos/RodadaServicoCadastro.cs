using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class RodadaServicoCadastro : IRodadaServicoCadastro
    {
        private readonly IRodadaRepositorio _rodadaRepositorio;

        public RodadaServicoCadastro(IRodadaRepositorio rodadaRepositorio)
        {
            _rodadaRepositorio = rodadaRepositorio;
        }

        public void CadastrarRodada(Rodada rodada)
        {
            if (_rodadaRepositorio.RodadaExistente(rodada.Nome, rodada.Grupo.Id))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Rodada_Class, Field.Rodada_Nome_Field, rodada.Nome));
            }

            _rodadaRepositorio.Insert(rodada);
        }
    }
}
