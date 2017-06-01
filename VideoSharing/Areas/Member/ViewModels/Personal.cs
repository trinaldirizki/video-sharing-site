using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoSharing.Models;

namespace VideoSharing.Areas.Member.ViewModels
{

        public class PersonalIndex
        {
            public IEnumerable<User> Users { get; set; } 
        }

        public class PersonalEdit
        {
            [Required, MaxLength(128)]
            public string NickName { get; set; }

            [Required]
            [MaxLength(256)]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            
        }

        public class PersonalResetPassword
        {
            public string NickName { get; set; }
            [Required, DataType(DataType.Password)]
            public string Password { get; set; }
        }
}