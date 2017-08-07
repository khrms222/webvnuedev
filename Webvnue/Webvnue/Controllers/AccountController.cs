using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Webvnue.ViewModels.Models;

namespace Webvnue.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Register()
        {
            RegisterViewModel vm = new RegisterViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel vm)
        {

            if (!ModelState.IsValid)
            {

                return View(vm);
            }
            else
            {
                vm.HandleRequest();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Login()
        {
            LoginViewModel vm = new LoginViewModel();


            return View(vm);
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                if (vm.loginVerified())
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, vm.email)
                    };

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    var context = Request.GetOwinContext();
                    var authManager = context.Authentication;

                    authManager.SignIn(new AuthenticationProperties
                    { IsPersistent = vm.rememberme }, identity);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("error", "Invalid email or password");
                    return View(vm);
                }
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }

    }
}