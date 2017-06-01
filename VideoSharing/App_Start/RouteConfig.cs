using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VideoSharing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute("Trending", "trending", new { controller = "Home", action = "Trending" });
            routes.MapRoute("UserLogin", "login", new { controller = "User", action = "Login" });
            routes.MapRoute("UserSignUp", "signup", new { controller = "User", action = "SignUp" });
            routes.MapRoute("UserLogout", "logout", new { controller = "User", action = "Logout" });
            routes.MapRoute("Upload", "upload", new { controller = "Upload", action = "Upload" });
            routes.MapRoute("Video", "video", new { controller = "VideoData", action = "Index"});
            routes.MapRoute("Download", "download", new { controller = "VideoData", action = "Download" });
            routes.MapRoute("Like", "like", new { controller = "VideoData", action = "LikeVideo" });
            routes.MapRoute("Search", "search", new { controller = "Search", action = "Index" });
            
        }
    }
}
