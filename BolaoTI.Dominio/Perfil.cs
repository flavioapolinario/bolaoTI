using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Perfil
    {
        private Guid _id;
        private string _nome;
        private List<Usuario> _usuarios;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }
    }
}
