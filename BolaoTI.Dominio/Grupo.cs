using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Grupo
    {
        private int _id;
        private int _faseId;
        private string _nome;
        private List<Rodada> _rodadas;
        private Fase _fase;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int FaseId
        {
            get { return _faseId; }
            set { _faseId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public List<Rodada> Rodadas
        {
            get { return _rodadas; }
            set { _rodadas = value; }
        }

        public Fase Fase
        {
            get { return _fase; }
            set { _fase = value; }
        }

    }
}
