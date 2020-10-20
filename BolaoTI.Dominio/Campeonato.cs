using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Campeonato
    {
        private int _id;
        private string _nome;
        private string _nomeAbreviado;
        private DateTime _inicio;
        private DateTime _fim;
        private List<Fase> _fases;
        private List<Organizacao> _organizacoes;
        private List<Ranking> _rankings;
               
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

        public DateTime Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }

        public DateTime Fim
        {
            get { return _fim; }
            set { _fim = value; }
        }

        public List<Fase> Fases
        {
            get { return _fases; }
            set { _fases = value; }
        }

        public List<Organizacao> Organizacoes
        {
            get { return _organizacoes; }
            set { _organizacoes = value; }
        }

        public List<Ranking> Rankings
        {
            get { return _rankings; }
            set { _rankings = value; }
        }

        public string Temporada
        {
            get
            {
                if (_inicio.Year != _fim.Year)
                    return String.Format(BolaoTI.Resources.Field.Campeonato_Temporada_Field, _inicio.Year.ToString(), _fim.Year.ToString());
                else
                    return String.Format(BolaoTI.Resources.Field.Campeonato_Temporada_Field, _inicio.Year.ToString());
            }
        }
    }
}