using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoSharing.Models;

namespace VideoSharing.Areas.Admin.ViewModels
{
    public class VideosEdit
    {
        [Required, MaxLength(128)]
        public string Title { get; set; }
    }


}