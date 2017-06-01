using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.CustomDataResult;
using VideoSharing.Models;
using VideoSharing.ViewModels;

namespace VideoSharing.Controllers
{
    public class VideoDataController : Controller
    {
        public ActionResult Index(int id)
        {
            var selectedVideo = Database.Session.Get<Video>(id);
            selectedVideo.view_count = selectedVideo.view_count + 1;

            var videoModel = new Videos()
            {
                path = selectedVideo.video_path,
                title = selectedVideo.video_title,
                likeCount = selectedVideo.like_count,
                viewCount = selectedVideo.view_count,
                id = selectedVideo.video_id,
                uploadDate = selectedVideo.video_load_date,
                uploader = selectedVideo.user.NickName
            };

            var videoIndexModel = new VideosIndex
            {
                Videos = Database.Session.Query<Video>()
            };

            var videoAndListModel = new VideoAndList
            {
                video = videoModel,
                videoList = videoIndexModel
            };

            Database.Session.Save(selectedVideo);
            Database.Session.Flush();
            return View(videoAndListModel);
        }

        [HttpGet]
        public ActionResult Download(int id)
        {
            foreach (var video in Database.Session.Query<Video>())
            {
                if (video.video_id == id)
                {
                    var videoData = new VideoDataResult
                    {
                        videoPath = video.video_path,
                        videoTitle = video.video_title
                    };
                    return videoData;
                }
            }


            return HttpNotFound();
        }
        
        public ActionResult LikeVideo(int id)
        {
            var selectedVideo = Database.Session.Get<Video>(id);
            selectedVideo.like_count = selectedVideo.like_count + 1;

            Database.Session.Save(selectedVideo);
            Database.Session.Flush();
            return RedirectToAction("Index", "VideoData", new { id = selectedVideo.video_id });
        }
    }
}