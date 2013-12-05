using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestTask.WebUI {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
              "Admin/{category}/Page{page}",
              new { controller = "UserAdministration", action = "Index" },
              new { page = @"\d+" }
            );

            routes.MapRoute(null,
              "Admin/Page{page}",
              new { controller = "UserAdministration", action = "Index", category = (bool?)null },
              new { page = @"\d+" }
            );

            routes.MapRoute(null,
             "Admin",
             new { controller = "UserAdministration", action = "Index", category = (bool?)null }
            );

            routes.MapRoute(null,
             "Registration",
             new { controller = "Registration", action = "Index" }
            );

            routes.MapRoute(null, "",
                new { controller = "Home", action = "Index", }
             );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}