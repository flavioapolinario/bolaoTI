using BolaoTI.Utils.Localizacao;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Aposta
    {
        private int _id;
        private Usuario _usuario;
        private Partida _partidaApostada;
        private int _golsTimeHome;
        private int _golsTimeAway;
        private int? _pontosAposta;
        private Guid _usuarioId;
        private int _partidaId;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }

        public int PartidaId
        {
            get { return _partidaId; }
            set { _partidaId = value; }
        }

        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Partida PartidaApostada
        {
            get { return _partidaApostada; }
            set { _partidaApostada = value; }
        }

        public int GolsTimeHome
        {
            get { return _golsTimeHome; }
            set { _golsTimeHome = value; }
        }

        public int GolsTimeAway
        {
            get { return _golsTimeAway; }
            set { _golsTimeAway = value; }
        }

        public int? PontosAposta
        {
            get { return _pontosAposta; }
            set { _pontosAposta = value; }
        }

    }
}