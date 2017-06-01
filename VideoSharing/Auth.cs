using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoSharing.Models;
using NHibernate.Linq;

namespace VideoSharing
{
    public static class Auth
    {
        public static User User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                var user = HttpContext.Current.Items["UserKey"] as User;

                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(p => p.Email == HttpContext.Current.User.Identity.Name);
                    Database.Session.Flush();

                }
                
                if (user == null)
                {
                    return null;
                }
                
                HttpContext.Current.Items["UserKey"] = user;

                return user;
            }
        }
    }
}