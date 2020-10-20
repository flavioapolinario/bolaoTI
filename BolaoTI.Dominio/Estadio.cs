using System;
using System.Collections.Generic;
using System.Linq;

namespace BolaoTI.Dominio
{
    public class Estadio
    {
        private int _id;
        private string _nome;
        private string _cidade;
        private string _uf;
        private int _capacidade;
        private List<Partida> _partidas;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        
        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        
        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }
        
        public int Capacidade
        {
            get { return _capacidade; }
            set { _capacidade = value; }
        }

        public string Localizacao
        {
            get
            {
                return string.Format("{0} - {1}", this._cidade, this._uf);
            }
        }

        public List<Partida> Partidas
        {
            get { return _partidas; }
            set { _partidas = value; }
        }
    }
}