using BolaoTI.Utils.Localizacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{

    public class Partida
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Display(Name = "Data da Partida")]
        [LocalizadoDisplayFormatAttribute( typeof(BolaoTI.Resources.RegularExpression), DataFormatString = "FormatString_DateTime", ApplyFormatInEditMode = true)]
        public DateTime DataPartida { get; set; }

        [ForeignKey("TimeHome")]
        public int TimeHomeID { get; set; }

        [Display(Name = "Time Casa")]
        public Time TimeHome { get; set; }

        [Display(Name = "Gol Time Casa")]
        public Nullable<int> GolsTimeHome { get; set; }

        [ForeignKey("TimeAway")]
        public int TimeAwayID { get; set; }

        [Display(Name = "Time Visitante")]
        public Time TimeAway { get; set; }

        [Display(Name = "Gol Time Visitante")]
        public Nullable<int> GolsTimeAway { get; set; }

        [ForeignKey("EstadioJogo")]
        public virtual int EstadioID { get; set; }

        [Display(Name = "Estadio")]
        public Estadio EstadioJogo { get; set; }

        [ForeignKey("Rodada")]
        public virtual int RodadaID { get; set; }

        [Display(Name = "Rodada")]
        public virtual Rodada Rodada { get; set; }

        [Display(Name = "Rodada - Grupo")]
        public string GrupoRodada
        {
            get
            {
                if (this.Rodada != null)
                {
                    return this.Rodada.NomeGrupo;
                }
                return string.Empty;
            }
        }

        [Display(Name = "Apostas da Partida")]
        public virtual ICollection<Aposta> Apostas { get; set; }
    }
}