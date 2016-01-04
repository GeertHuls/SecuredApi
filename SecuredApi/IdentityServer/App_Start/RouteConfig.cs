﻿using System.Web.Mvc;
using System.Web.Routing;

namespace IdentityServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "core/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional } );
        }
    }
}