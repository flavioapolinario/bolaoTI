using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{    
    public class Grupo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name="Nome do Grupo")]
        public string Nome { get; set; }

        [Display(Name = "Rodadas do Grupo")]
        public virtual ICollection<Rodada> Rodadas { get; set; }

        [ForeignKey("Fase")]
        public int FaseID { get; set; }

        [Display(Name = "Fase do Grupo")]
        public virtual Fase Fase { get; set; }

    }
}
