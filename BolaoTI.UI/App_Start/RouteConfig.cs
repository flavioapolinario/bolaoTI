﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BolaoTI.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("RecuperarCampeonatosPorIdOrganizacao",
                           "Aposta/RecuperarCampeonatosPorIdOrganizacao/",
                           new { controller = "Aposta", action = "RecuperarCampeonatosPorIdOrganizacao" },
                           new[] { "BolaoTI.UI.Controllers" });
        }
    }
}