using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Time
    {
        private int _id;
        private string _nome;
        private string _nomeAbreviado;
        private string _imagemBandeira;
        private ICollection<Partida> _partidasHome;
        private ICollection<Partida> _partidasAway;

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

        public string NomeAbreviado
        {
            get { return _nomeAbreviado; }
            set { _nomeAbreviado = value; }
        }

        public string ImagemBandeira
        {
            get { return _imagemBandeira; }
            set { _imagemBandeira = value; }
        }

        public ICollection<Partida> PartidasHome
        {
            get { return _partidasHome; }
            set { _partidasHome = value; }
        }

        public ICollection<Partida> PartidasAway
        {
            get { return _partidasAway; }
            set { _partidasAway = value; }
        }
    }
}