﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PIDS_Final
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "File",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WordFile", action = "DisplayWord", id = UrlParameter.Optional }
            );
        }
    }
}
