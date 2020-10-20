using BolaoTI.Utils.Localizacao;
using System;
using System.Collections.Generic;

namespace BolaoTI.Dominio
{
    public class Fase
    {
        private int _id;
        private int _campeonatoId;
        private string _nome;
        private DateTime _dataInicio;
        private DateTime _dataFim;
        private List<Grupo> _grupos;
        private Campeonato _campeonato;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CampeonatoId
        {
            get { return _campeonatoId; }
            set { _campeonatoId = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public DateTime DataInicio
        {
            get { return _dataInicio; }
            set { _dataInicio = value; }
        }

        public DateTime DataFim
        {
            get { return _dataFim; }
            set { _dataFim = value; }
        }

        public List<Grupo> Grupos
        {
            get { return _grupos; }
            set { _grupos = value; }
        }

        public Campeonato Campeonato
        {
            get { return _campeonato; }
            set { _campeonato = value; }
        }

        /// <summary>
        /// Situação de Palpites da fase
        /// Se a data de encerramento é menor que a data atual, é permitido atualizar/salvar palpites
        /// caso contrario desabilita a edição
        /// </summary>                
        public bool EstaAberta
        {
            get
            {
                return ((BolaoTI.Utils.FieldUtil.DataAtualFusoHorario() >= _dataInicio) &&
                       (BolaoTI.Utils.FieldUtil.DataAtualFusoHorario() <= _dataFim) );
            }
        }

    }
}