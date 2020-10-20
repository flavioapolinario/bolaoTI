using BolaoTI.Utils.Localizacao;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Partida
    {
        private int _id;
        private int _estadioId;
        private int _timeHomeId;
        private int _timeAwayId;
        private int _rodadaId;
        private DateTime _dataPartida;
        private Nullable<int> _golsTimeHome;
        private Nullable<int> _golsTimeAway;

        private Time _timeHome;
        private Time _timeAway;
        private Estadio _estadio;
        private Rodada _rodada;
        private List<Aposta> _apostas;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int EstadioId
        {
            get { return _estadioId; }
            set { _estadioId = value; }
        }

        public int TimeHomeId
        {
            get { return _timeHomeId; }
            set { _timeHomeId = value; }
        }

        public int TimeAwayId
        {
            get { return _timeAwayId; }
            set { _timeAwayId = value; }
        }        

        public int RodadaId
        {
            get { return _rodadaId; }
            set { _rodadaId = value; }
        }

        public DateTime DataPartida
        {
            get { return _dataPartida; }
            set { _dataPartida = value; }
        }

        public Time TimeHome
        {
            get { return _timeHome; }
            set { _timeHome = value; }
        }

        public Nullable<int> GolsTimeHome
        {
            get { return _golsTimeHome; }
            set { _golsTimeHome = value; }
        }

        public Time TimeAway
        {
            get { return _timeAway; }
            set { _timeAway = value; }
        }

        public Nullable<int> GolsTimeAway
        {
            get { return _golsTimeAway; }
            set { _golsTimeAway = value; }
        }

        public Estadio Estadio
        {
            get { return _estadio; }
            set { _estadio = value; }
        }

        public Rodada Rodada
        {
            get { return _rodada; }
            set { _rodada = value; }
        }

        public List<Aposta> Apostas
        {
            get { return _apostas; }
            set { _apostas = value; }
        }

        public string GrupoRodada
        {
            get
            {
                if (this._rodada != null)
                {
                    return this._rodada.NomeGrupo;
                }
                return string.Empty;
            }
        }
    }
}