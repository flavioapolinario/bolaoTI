using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{
    public class Aposta
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Apostador")]
        public UserProfile Usuario { get; set; }

        [ForeignKey("PartidaApostada")]
        public int PartidaID { get; set; }

        [Display(Name = "Aposta da Partida")]
        public Partida PartidaApostada { get; set; }

        [Display(Name = "Gols do time da casa")]
        [DisplayFormat(NullDisplayText = "Sem gols")]
        public int GolsTimeHome { get; set; }

        [Display(Name = "Gols do time visitante")]
        [DisplayFormat(NullDisplayText = "Sem gols")]
        public int GolsTimeAway { get; set; }

        [Display(Name = "Pontos somados da aposta")]
        public int PontosAposta { get; set; }
    }
}