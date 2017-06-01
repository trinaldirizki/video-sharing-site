using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VideoSharing.Areas.Member.ViewModels
{
    public class MyVideosEdit
    {
        [Required, MaxLength(128)]
        public string title { get; set; }
    }
}