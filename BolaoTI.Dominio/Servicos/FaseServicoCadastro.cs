using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class FaseServicoCadastro : IFaseServicoCadastro
    {
        private readonly IFaseRepositorio _faseRepositorio;

        public FaseServicoCadastro(IFaseRepositorio faseRepositorio)
        {
            _faseRepositorio = faseRepositorio;
        }

        public void CadastrarFase(Fase fase)
        {
            if (_faseRepositorio.FaseExistente(fase.Nome, fase.Campeonato.Id))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Fase_Class, Field.Fase_Nome_Field, fase.Nome));
            }

            _faseRepositorio.Insert(fase);
        }
    }
}
