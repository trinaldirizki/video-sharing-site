using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace VideoSharing.CustomDataResult
{
    public class VideoDataResult : ActionResult
    {
        public virtual string videoPath { get; set; }
        public virtual string videoTitle { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var strVideoFilePath = HostingEnvironment.MapPath(videoPath);
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + videoTitle);
            
            var objFile = new FileInfo(strVideoFilePath);
            var stream = objFile.OpenRead();
            var objBytes = new byte[stream.Length];
            stream.Read(objBytes, 0, (int)objFile.Length);
            context.HttpContext.Response.BinaryWrite(objBytes);
        }
    }
}