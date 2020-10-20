using System.ComponentModel.DataAnnotations;

namespace BolaoTI.UI.ViewsModel
{
    public class TimeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Time_Nome_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(200, ErrorMessageResourceName = "ErrorMessage_Time_TamanhoNome", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Time_NomeAbreviado_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(3, ErrorMessageResourceName = "ErrorMessage_Time_TamanhoNomeAbreviado", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string NomeAbreviado { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Time_Bandeira_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string BandeiraCaminho { get; set; }
    }
}