using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class TimeServicoCadastro : ITimeServicoCadastro
    {
        private readonly ITimeRepositorio _timeRepositorio;

        public TimeServicoCadastro(ITimeRepositorio timeRepositorio)
        {
            _timeRepositorio = timeRepositorio;
        }

        public void CadastrarTime(Time time)
        {
            if (_timeRepositorio.TimeExistente(time.Nome))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Time_Class, Field.Time_Nome_Field, time.Nome));
            }

            _timeRepositorio.Insert(time);
        }
    }
}
