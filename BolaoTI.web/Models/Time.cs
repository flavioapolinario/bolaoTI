using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{
    public class Time
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name="Nome")]
        [StringLength(50, ErrorMessage = "Nome do time deve ter no maximo {0} caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Nome Abreviado")]
        [StringLength(3, ErrorMessage = "Nome abreviado do time deve ter no maximo {0} caracteres.")]
        public string NomeAbreviado { get; set; }

        [Display(Name = "Bandeira")]
        public string ImagemBandeira { get; set; } 
    }
}