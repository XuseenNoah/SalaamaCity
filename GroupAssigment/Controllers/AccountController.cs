using GroupAssigment.Models;
using GroupAssigment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GroupAssigment.Helpers;

namespace GroupAssigment.Controllers
{
    public class AccountController : Controller
    {
       Login_Repository _repository = new Login_Repository();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {
                //var decrpt = FormsAuthentication.HashPasswordForStoringInConfigFile(loginForm.Password, "SHA1");
              var user = _repository.Authenticate(loginForm.Username, loginForm.Password);
                if (user != null)
                {
                    Session["User"] = user;
                    HttpContext.Cache[string.Format("{0}'s CurrentPermision", loginForm.Username)] = user.CurrentPermissions;
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["Users"] = loginForm.Username;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Username or Password.";
                }
            }
            return View(loginForm);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            TempData["SuccessMessage"] = "You have been logged out of the system.";
            return RedirectToAction("Login");
        }
        [PermisionRequired(LoginForm.Permissions.Delete)]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(LoginForm login)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateUser(login);
                TempData["suc"] = "Succesfully Saved";
            }
            return View();
        }
    }
}