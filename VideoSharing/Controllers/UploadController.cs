using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NHibernate.Linq;
using VideoSharing.Models;
using VideoSharing.ViewModels;

namespace VideoSharing.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        [Authorize(Roles = "member")]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase postedFile, Videos videoForm)
        {

            string path = "";
            try
            {
                if (postedFile != null)
                {
                    if (videoForm.title != null)
                    {
                        if (Path.GetExtension(postedFile.FileName).ToLower() == ".mp4"
                        || Path.GetExtension(postedFile.FileName).ToLower() == ".webm"
                        || Path.GetExtension(postedFile.FileName).ToLower() == ".mpeg"
                        || Path.GetExtension(postedFile.FileName).ToLower() == ".m4v"
                        || Path.GetExtension(postedFile.FileName).ToLower() == ".wmv")
                        {

                            path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(postedFile.FileName));
                            postedFile.SaveAs(path);

                            // Use relative path
                            string[] splittedString = path.ToString().Split('\\');
                            string relativePath = "~\\UploadedFiles\\" + splittedString[splittedString.Length-1];

                            var video = new Video
                            {
                                video_title = videoForm.title,
                                video_path = relativePath,
                                user = Auth.User,
                                video_load_date = DateTime.Now,
                                like_count = 0,
                                view_count = 0
                            };

                            Database.Session.Save(video);
                            ViewBag.Message = "File uploaded successfully.";
                            
                        }
                        else
                            ViewBag.Message = "Lütfen geçerli bir video formatı giriniz!(Geçerli video formatları : mp4,webm,mpeg,m4v)";
                    }
                    else
                        ViewBag.Message = "Title Giriniz!";
                }

                else
                    ViewBag.Message = "Lütfen bir dosya seçin!";
            }
            catch (Exception e)
            {
                ViewBag.FileStatus = "Error while file uploading." + e;
            }

            return View("Upload");
            
        }
    }
}