using BolaoTI.web.BLL;
using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BolaoTI.web.ViewModels
{
    public class ApostaPartidaData
    {
        public int? IdUsuario { get; set; }
        public int? IdAposta { get; set; }
        public int IdFase { get; set; }
        public int IdGrupo { get; set; }
        public int IdRodada { get; set; }
        public int IdPartida { get; set; }
        public int IdTimeHome { get; set; }
        public int IdTimeAway { get; set; }
        public int IdEstadio { get; set; }

        public string NomeUsuario{ get; set; }

        public DateTime? DataEncerramentoFase { get; set; }
        public bool EstaAberta
        {
            get
            {
                if (!DataEncerramentoFase.HasValue)
                    return true;

                return DataEncerramentoFase.Value.CompareTo(BolaoTI.web.BLL.Utils.DataAtualFusoHorario()) < 0;
            }
        }

        public DateTime DataPartida { get; set; }
        public bool PartidaEncerrada
        {
            get
            {
                return DataPartida.CompareTo(BolaoTI.web.BLL.Utils.DataAtualFusoHorario()) < 0;
            }
        }

        public string NomeTimeHome { get; set; }
        public string NomeAbreviadoTimeHome { get; set; }
        public string ImageTimeHome { get; set; }

        public string NomeTimeAway { get; set; }
        public string NomeAbreviadoTimeAway { get; set; }
        public string ImageTimeAway { get; set; }

        public string NomeFase { get; set; }
        public string NomeGrupo { get; set; }
        public string NomeRodada { get; set; }

        public string NomeEstadio { get; set; }

        [Required(ErrorMessage = "Informe o Nº de gols")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Formato invalido")]
        public int? GolsApostadoTimeHome { get; set; }

        [Required(ErrorMessage = "Informe o Nº de gols")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Formato invalido")]
        public int? GolsApostadoTimeAway { get; set; }        
    }
}