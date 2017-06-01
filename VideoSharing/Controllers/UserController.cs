using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoSharing.ViewModels;
using NHibernate.Linq;
using VideoSharing.Models;
using System.Web.Security;
using VideoSharing.Infrastructure;

namespace VideoSharing.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [SelectedTab("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [SelectedTab("login")]
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

        [SelectedTab("signup")]
        public ActionResult SignUp()
        {
            return View(new UserSignUp());
        }

        [HttpPost]
        [SelectedTab("signup")]
        public ActionResult SignUp(UserSignUp signUpForm)
        {
            if (Database.Session.Query<User>().Any(p => p.Email == signUpForm.Email))
            {
                ModelState.AddModelError("Email", "Email must be unique");
            } //username control in database. 

            if (signUpForm.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Passwords must be longer then 8 characters");
            }

            if (signUpForm.Password != signUpForm.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordAgain", "Passwords are not the same");
            }

            char[] passwordchars = signUpForm.Password.ToCharArray();
            for (int i = 0; i < passwordchars.Length; i++)
            {
                if (passwordchars[i] >= 48 && passwordchars[i] <= 57) break;

                if (i + 1 == passwordchars.Length)
                {
                    ModelState.AddModelError("Password", "Password must contain at 1 least number");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(signUpForm);
            }

            var user = new User
            {
                Name = signUpForm.Name,
                Surname = signUpForm.Surname,
                NickName = signUpForm.NickName,
                Email = signUpForm.Email,

            };
            //SyncRoles(signUpForm.Roles, user.Roles);
            user.SetPassword(signUpForm.Password);
            Database.Session.Save(user);
            Database.Session.Flush();
            MyMail.Send(signUpForm);
            return RedirectToRoute("Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}