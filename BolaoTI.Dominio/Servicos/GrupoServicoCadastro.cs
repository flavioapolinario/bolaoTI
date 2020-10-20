using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class GrupoServicoCadastro : IGrupoServicoCadastro
    {
        private readonly IGrupoRepositorio _grupoRepositorio;

        public GrupoServicoCadastro(IGrupoRepositorio grupoRepositorio)
        {
            _grupoRepositorio = grupoRepositorio;
        }

        public void CadastrarGrupo(Grupo grupo)
        {
            if (_grupoRepositorio.GrupoExistente(grupo.Nome, grupo.Fase.Id))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Grupo_Class, Field.Grupo_Nome_Field, grupo.Nome));
            }

            _grupoRepositorio.Insert(grupo);
        }
    }
}
