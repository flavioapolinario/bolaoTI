using System;
namespace BolaoTI.Dominio
{
    public class Ranking
    {
        //private int _id;
        private int _organizacaoId;
        private int _campeonatoId;       
        private Guid _usuarioId;           
        private int _numeroApostas;
        private int _numeroPartidas;
        private int _totalPontos;
        private int _dezTotalPontos;
        private int _seteTotalPontos;
        private int _cincoTotalPontos;
        private int _doisTotalPontos;
        private int _colocacao;

        private Organizacao _organizacao;
        private Campeonato _campeonato;
        private Usuario _usuario;

        //public int Id
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        public int OrganizacaoId
        {
            get { return _organizacaoId; }
            set { _organizacaoId = value; }
        }

        public int CampeonatoId
        {
            get { return _campeonatoId; }
            set { _campeonatoId = value; }
        }

        public Guid UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }

        public Organizacao Organizacao
        {
            get { return _organizacao; }
            set { _organizacao = value; }
        }

        public Campeonato Campeonato
        {
            get { return _campeonato; }
            set { _campeonato = value; }
        }

        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public int NumeroApostas
        {
            get { return _numeroApostas; }
            set { _numeroApostas = value; }
        }

        public int NumeroPartidas
        {
            get { return _numeroPartidas; }
            set { _numeroPartidas = value; }
        }

        public int TotalPontos
        {
            get { return _totalPontos; }
            set { _totalPontos = value; }
        }

        public int DezTotalPontos
        {
            get { return _dezTotalPontos; }
            set { _dezTotalPontos = value; }
        }

        public int SeteTotalPontos
        {
            get { return _seteTotalPontos; }
            set { _seteTotalPontos = value; }
        }

        public int CincoTotalPontos
        {
            get { return _cincoTotalPontos; }
            set { _cincoTotalPontos = value; }
        }

        public int DoisTotalPontos
        {
            get { return _doisTotalPontos; }
            set { _doisTotalPontos = value; }
        }

        public int Colocacao
        {
            get { return _colocacao; }
            set { _colocacao = value; }
        }

    }
}
