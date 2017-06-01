using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.Areas.Member.ViewModels;
using VideoSharing.Infrastructure;
using NHibernate.Linq;
using VideoSharing.Models;

namespace VideoSharing.Areas.Member.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Member/Personal
        [Authorize(Roles = "member")]
        [SelectedTab("personal")]
        public ActionResult Index()
        {
            return View(new PersonalIndex
            {
                Users = Database.Session.Query<User>().ToList().Where(p => p.Email == Auth.User.Email)

            });
        }

        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            return View(new PersonalEdit
                {
                    NickName = user.NickName,
                    Email = user.Email
                });
        }

        [HttpPost]
        public ActionResult Edit(int id, PersonalEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if (Database.Session.Query<User>().Any(u => u.Email == form.Email && u.user_id != id))
                ModelState.AddModelError("Email", "Email must be unique");

            if (!ModelState.IsValid)
                return View(form);

            user.NickName = form.NickName;
            user.Email = form.Email;
            Database.Session.Update(user);
            Database.Session.Flush();

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            return View(new PersonalResetPassword
            {
                NickName = user.NickName,
               
            });
        }


        [HttpPost]
        public ActionResult ResetPassword(int id, PersonalResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            form.NickName = user.NickName;


            if (!ModelState.IsValid)
                return View(form);
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