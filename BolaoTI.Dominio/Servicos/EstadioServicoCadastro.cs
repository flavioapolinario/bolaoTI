using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class EstadioServicoCadastro : IEstadioServicoCadastro
    {
        private readonly IEstadioRepositorio _estadioRepositorio;

        public EstadioServicoCadastro(IEstadioRepositorio estadioRepositorio)
        {
            _estadioRepositorio = estadioRepositorio;
        }

        public void CadastrarEstadio(Estadio estadio)
        {
            if (_estadioRepositorio.EstadioExistente(estadio.Nome))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Estadio_Class, Field.Estadio_Nome_Field, estadio.Nome));
            }

            _estadioRepositorio.Insert(estadio);
        }
    }
}
