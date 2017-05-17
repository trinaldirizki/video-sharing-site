using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.ViewModels;
using NHibernate.Linq;
using VideoSharing.Models;
using System.Web.Security;

namespace VideoSharing.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(UserLogin loginForm)
        //{
        //    if (loginForm.email == "fidel")
        //        if (loginForm.password == "1234")
        //            return RedirectToRoute("Home");
        //    return View(loginForm);
        //}

        [HttpPost]
        public ActionResult Login(UserLogin form, string returnUrl)
        {

            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Email == form.email);

            if (user == null)
            {
                VideoSharing.Models.User.FakeHash();
            }

            if (user == null || !user.CheckPassword(form.password))
            {
                ModelState.AddModelError("Username", "Username or password is invalid !");
            }


            if (!ModelState.IsValid)
            {
                return View(form);
            }


            FormsAuthentication.SetAuthCookie(form.email, true);


            if (!String.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute("Home");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }

        public ActionResult SignUp()
        {
            return View(new UserSignUp());
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUp signUpForm)
        {
            if (Database.Session.Query<User>().Any(p => p.NickName == signUpForm.NickName))
            {
                ModelState.AddModelError("Nickname", "Nickname must be unique");
            } //username control in database. 

            if(!ModelState.IsValid)
            {
                return View(signUpForm);
            }

            var user = new User
            {
                Name = signUpForm.Ad,
                Surname = signUpForm.Soyad,
                NickName = signUpForm.NickName,
                Email = signUpForm.Email,
                
            };
            user.SetPassword(signUpForm.password);
            Database.Session.Save(user);
            return RedirectToRoute("Home");
        }
    }
}