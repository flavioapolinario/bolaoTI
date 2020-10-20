using BolaoTI.web.BLL;
using BolaoTI.web.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BolaoTI.web
{
    public class AppInfoActionFilter : ActionFilterAttribute
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            UnitOfWork unitOfWorkAux = new UnitOfWork();
            if (WebSecurity.IsAuthenticated)
            {
                int numParticipantes = unitOfWork.UsuarioRepository.Get().Count();
                filterContext.Controller.ViewBag.NumeroParticipantes = numParticipantes.ToString();
                filterContext.Controller.ViewBag.TotalArrecadado = ((numParticipantes * 20)).ToString("C");
                string ExhibitionName;
                try
                {
                    //ExhibitionName = unitOfWork.UsuarioRepository.GetByID(WebSecurity.CurrentUserId).ExhibitionName;
                    ExhibitionName = unitOfWorkAux.UsuarioRepository.Get(filter: u => u.UserId.Equals(WebSecurity.CurrentUserId)).First().ExhibitionName;
                }
                catch(Exception)
                {
                    ExhibitionName = String.Empty;
                }
                filterContext.Controller.ViewBag.ExhibitionName = ExhibitionName;
                filterContext.Controller.ViewBag.DataAtual = BolaoTI.web.BLL.Utils.DataAtualFusoHorario().ToString("dd/MM/yyyy HH:mm");

            }
        }

    }
}