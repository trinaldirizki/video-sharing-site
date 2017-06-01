using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.Areas.Admin.ViewModels;
using VideoSharing.Infrastructure;
using VideoSharing.Models;
using NHibernate.Linq;
using System.Web.Security;

namespace VideoSharing.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        [Authorize(Roles = "admin")]
        [SelectedTab("users")]
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }

        private void SyncRoles(IList<RoleCheckBox> checkBoxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in Database.Session.Query<Role>())
            {
                var checkBox = checkBoxes.Single(c => c.Id == role.roles_id);
                checkBox.Name = role.Name;

                if (checkBox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var toAdd in selectedRoles.Where(p => !roles.Contains(p)))
            {
                roles.Add(toAdd);
            }

            foreach (var toRemove in roles.Where(p => !selectedRoles.Contains(p)).ToList())
            {
                roles.Remove(toRemove);
            }

        } 


        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null) return HttpNotFound();

            return View(new UsersEdit
                {
                    NickName=user.NickName,
                    Email= user.Email,

                    Roles = Database.Session.Query<Role>().Select(
                       role => new RoleCheckBox()
                       {
                           Id = role.roles_id,
                           Name = role.Name,
                           IsChecked = user.Roles.Contains(role)

                       }).ToList()

                });
        }

        [HttpPost]
        public ActionResult Edit(int id, UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);

            if (user == null) return HttpNotFound();

            if (Database.Session.Query<User>().Any(p => p.Email == form.Email && p.user_id != id))
            {
                ModelState.AddModelError("Email", "Email must be unique");
            } //username control in database. 

            if (!ModelState.IsValid) //form validation control
            {
                return View(form);
            }
            
            user.NickName = form.NickName;
            if (user.user_id == Auth.User.user_id)
            {
                if (form.Email != Auth.User.Email)
                {
                    FormsAuthentication.SignOut();
                }
            }
            user.Email = form.Email;
            
            SyncRoles(form.Roles, user.Roles);

            Database.Session.Update(user); //save user object to database
            Database.Session.Flush();
            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null) return HttpNotFound();
            return View(new UsersResetPassword
            {
                NickName = user.NickName
            });
        }


        [HttpPost]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null) return HttpNotFound();

            form.NickName = user.NickName;

            if (!ModelState.IsValid) return View(form);

            user.SetPassword(form.Password);
            Database.Session.Update(user);
            Database.Session.Flush();

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null) return HttpNotFound();

            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
       
    }
}