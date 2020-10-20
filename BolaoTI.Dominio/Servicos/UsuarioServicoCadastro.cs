using BolaoTI.Dominio.Exceptions;
using BolaoTI.Dominio.Interfaces.Aplicacao;
using BolaoTI.Dominio.Interfaces.Repositorios;
using BolaoTI.Resources;

namespace BolaoTI.Dominio.Servicos
{
    public class UsuarioServicoCadastro : IUsuarioServicoCadastro
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServicoCadastro(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if (_usuarioRepositorio.UsuarioExistente(usuario.Email))
            {
                throw new BolaoTIException(string.Format(Messages.AlertMessage_Registro_Existente, Classes.Usuario_Class, Field.Usuario_Nome_Field + " - " + Field.Usuario_Email_Field, usuario.Nome + " - " + usuario.Email));
            }

            _usuarioRepositorio.Insert(usuario);
        }
    }
}
