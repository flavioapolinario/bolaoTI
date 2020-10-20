using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BolaoTI.UI.ViewsModel
{
    public class UsuarioViewModel
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Usuario_Nome_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [StringLength(256, ErrorMessageResourceName = "ErrorMessage_Time_TamanhoNome", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Usuario_Email_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        [EmailAddress(ErrorMessageResourceName = "ErrorMessage_Comum_EmailInvalidoField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages), ErrorMessage = null)]
        public string Email { get; set; }

        [Display(Name = "Usuario_Telefone_Field", ResourceType = typeof(BolaoTI.Resources.Field))]
        public string Telefone { get; set; }

        public IEnumerable<SelectListItem> PerfilList { get; set; }

        public IEnumerable<SelectListItem> OrganizacoesList { get; set; }
    }
}