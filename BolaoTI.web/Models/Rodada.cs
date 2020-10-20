using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{
    public class Rodada
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [Display(Name = "Rodada")]
        public string Nome { get; set; }

        [Display(Name = "Partidas")]
        public virtual ICollection<Partida> Partidas { get; set; }

        [ForeignKey("RodadaGrupo")]
        public virtual int GrupoID { get; set; }

        [Display(Name = "Rodada do Grupo")]
        public virtual Grupo RodadaGrupo { get; set; }

        public virtual string NomeGrupo
        {
            get
            {
                if (this.RodadaGrupo != null)
                {
                    return string.Format("{0} - {1}", this.Nome, this.RodadaGrupo.Nome);
                }
                return string.Empty;
            }
        }

    }
}