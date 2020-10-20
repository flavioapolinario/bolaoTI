using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Rodada
    {
        private int _id;
        private int _grupoId;
        private string _nome;
        private int _ordem;       
        private List<Partida> _partidas;
        private Grupo _grupo;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int GrupoId
        {
            get { return _grupoId; }
            set { _grupoId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Ordem
        {
            get { return _ordem; }
            set { _ordem = value; }
        }

        public List<Partida> Partidas
        {
            get { return _partidas; }
            set { _partidas = value; }
        }

        public Grupo Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        public string NomeGrupo
        {
            get
            {
                if (this._grupo != null)
                {
                    return string.Format("{0} - {1}", this._nome, this._grupo.Nome);
                }
                return string.Empty;
            }
        }

    }
}