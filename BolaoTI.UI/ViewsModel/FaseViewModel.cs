using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace BolaoTI.UI.ViewsModel
{
    public class FaseViewModel
    {
        public FaseViewModel()
        {
            Fases = new List<SelectListItem>();            
        }

        [Required(ErrorMessageResourceName = "ErrorMessage_Comum_EmptyField", ErrorMessageResourceType = typeof(BolaoTI.Resources.Messages))]
        [Display(Name = "Label_Selecione_Fase", ResourceType = typeof(BolaoTI.Resources.View.Label))]
        public int FaseId { get; set; }        
        public IList<SelectListItem> Fases { get; set; }

        public Dominio.Campeonato Campeonato { get; set; }

        public Dominio.Fase Fase { get; set; }
    }
}