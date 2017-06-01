using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoSharing.Models;

namespace VideoSharing.ViewModels
{
    public class Videos
    {
        [Required]
        public string title { get; set; }
        [Required(ErrorMessage = "Please choose file to upload")]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload file")]
        public string path { get; set; }
        public int likeCount { get; set; }
        public int viewCount { get; set; }
        public string uploader { get; set; }
        public int id { get; set; }
        public DateTime uploadDate { get; set; }
    }

    public class VideosIndex
    {
        public IEnumerable<Video> Videos { get; set; }
        public string searchvariable { get; set; }
    }

    public class VideoAndList : VideosIndex
    {
        public Videos video { get; set; }
        public VideosIndex videoList { get; set; }
    }
}