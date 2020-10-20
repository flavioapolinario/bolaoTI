using BolaoTI.web.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BolaoTI.web.Models
{
    public class Fase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Display(Name = "Data de Encerramento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DataEncerramento { get; set; }


        /// <summary>
        /// Situação de Palpites da fase
        /// Se a data de encerramento é menor que a data atual, é permitido atualizar/salvar palpites
        /// caso contrario desabilita a edição
        /// </summary>
        [Display(Name = "Situação")]
        public bool EstaAberta
        {
            get
            {
                if (!DataEncerramento.HasValue)
                    return true;

                return !(BolaoTI.web.BLL.Utils.DataAtualFusoHorario().CompareTo(DataEncerramento.Value) > 0);
            }
        }

        public virtual ICollection<Grupo> Grupos { get; set; }
    }
}