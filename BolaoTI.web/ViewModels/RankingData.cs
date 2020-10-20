using BolaoTI.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BolaoTI.web.ViewModels
{
    public class RankingData
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }     

        [Display(Name = "Participante")]
        public string Usuario { get; set; }

        [Display(Name = "Participante")]
        public string ExhibitionName { get; set; }

        [Display(Name = "Total Pts")]
        public int TotalPontos { get; set; }

        [Display(Name = "10 Pts")]
        public int DezTotalPontos { get; set; }

        [Display(Name = "7 Pts")]
        public int SeteTotalPontos { get; set; }

        [Display(Name = "5 Pts")]
        public int CincoTotalPontos { get; set; }

        [Display(Name = "2 Pts")]
        public int DoisTotalPontos { get; set; }

        [Display(Name = "Colocação")]
        public int Colocacao { get; set; }

        [Display(Name = "Nº Apostas")]
        public int NumeroApostas { get; set; }
    }
}