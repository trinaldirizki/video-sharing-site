using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.Models;
using VideoSharing.ViewModels;

namespace VideoSharing.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(VideosIndex search)
        {
            return View(new VideosIndex
            {
                Videos = Database.Session.QueryOver<Video>().WhereRestrictionOn(p => p.video_title).IsLike("%" + search.searchvariable + "%").List()
            });
        }
    }
}