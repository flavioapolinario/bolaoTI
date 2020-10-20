using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{
    public class Estadio
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Uf { get; set; }

        [Display(Name = "Localização")]
        public string Localizacao
        {
            get
            {
                return string.Format("{0} - {1}", this.Cidade, this.Uf);
            }
        }

        [Display(Name = "Capacidade")]
        public long Capacidade { get; set; }
    }
}