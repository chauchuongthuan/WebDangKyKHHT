using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDangKyKHHT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Trang chu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );
            routes.MapRoute(
                name: "MonHocs",
                url: "ke-hoach-dao-tao",
                defaults: new { controller = "MonHocs", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );
            routes.MapRoute(
                name: "Dang ky KHHT",
                url: "dang-ky-ke-hoach-hoc-tap",
                defaults: new { controller = "DangKyKHHT", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );
            routes.MapRoute(
                name: "Lich su dang ky",
                url: "lich-su-dang-ky",
                defaults: new { controller = "KHHTs", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );
            
            routes.MapRoute(
                name: "Edit KHHT",
                url: "chinh-sua-dang-ky",
                defaults: new { controller = "DangKyKHHT", action = "edit", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );
            routes.MapRoute(
                name: "Chi tiet dang ky",
                url: "chi-tiet-dang-ky-hoc-ki-{id}",
                defaults: new { controller = "KHHTs", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebDangKyKHHT.Controllers" }
            );

            
        }
    }
}
