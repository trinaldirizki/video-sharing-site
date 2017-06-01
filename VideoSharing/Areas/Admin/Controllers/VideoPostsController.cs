using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.Areas.Admin.ViewModels;
using VideoSharing.Infrastructure;
using VideoSharing.Models;
using VideoSharing.ViewModels;

namespace VideoSharing.Areas.Admin.Controllers
{
    public class VideoPostsController : Controller
    {
        [Authorize(Roles = "admin")]
        [SelectedTab("videopost")]
        // GET: Admin/VideoPosts
        public ActionResult Index()
        {
            return View(new VideosIndex
            {
                Videos = Database.Session.Query<Video>().ToList()
            });
        }

        public ActionResult Edit(int id)
        {
            var video = Database.Session.Load<Video>(id);
            if (video == null)
                return HttpNotFound();
            return View(new VideosEdit
            {
                Title = video.video_title,
                
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, VideosEdit form)
        {
            var video = Database.Session.Load<Video>(id);

            if (video == null) return HttpNotFound();

            if (id != video.video_id)
            {
                ModelState.AddModelError("Error!", "Id Numaraları eşleşmiyor!");
            }

            if (!ModelState.IsValid) //form validation control
            {
                return View(form);
            }
            
            video.video_title = form.Title;

            Database.Session.Update(video); //save user object to database
            Database.Session.Flush();
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var video = Database.Session.Load<Video>(id);
            if (video == null) return HttpNotFound();

            Database.Session.Delete(video);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
    }
}