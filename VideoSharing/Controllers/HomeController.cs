using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.CustomDataResult;
using VideoSharing.Infrastructure;
using VideoSharing.Models;
using VideoSharing.ViewModels;

namespace VideoSharing.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        [SelectedTab("home")]
        public ActionResult Index()
        {
            var videos = Database.Session.Query<Video>();
            return View(new VideosIndex() { Videos = videos });
        }

        [SelectedTab("trending")]
        public ActionResult Trending()
        {
            var videos = Database.Session.Query<Video>().OrderByDescending(p => p.view_count);
            return View(new VideosIndex() { Videos = videos });
        }
        
    }
}