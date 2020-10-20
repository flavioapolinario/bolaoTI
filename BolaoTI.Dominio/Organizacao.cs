using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Organizacao
    {
        private int _id;
        private string _nome;
        private List<Usuario> _usuarios;
        private List<Campeonato> _campeonatos;
        private List<Ranking> _rankings;
        
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }

        public List<Campeonato> Campeonatos
        {
            get { return _campeonatos; }
            set { _campeonatos = value; }
        }

        public List<Ranking> Rankings
        {
            get { return _rankings; }
            set { _rankings = value; }
        }

    }
}