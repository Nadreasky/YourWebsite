using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YourWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DanhMuc",
                url: "DanhMuc/{id}",
                defaults: new { controller = "DanhMuc", action = "Index" }
            );
            routes.MapRoute(
                name: "SanPhamDetail",
                url: "Sanpham/{id}",
                defaults: new { controller = "Sanpham", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
