using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BolaoTI.UI.ViewsModel
{
    public class EstadioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Estadio_Nome_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(200, ErrorMessage = "Maximo de 200 Caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Estadio_Cidade_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(200, ErrorMessage = "Maximo de 200 Caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Estadio_Estado_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(2, ErrorMessage = "Duas Letras")]
        public string Uf { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Estadio_Capacidade_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public int Capacidade { get; set; }
    }
}