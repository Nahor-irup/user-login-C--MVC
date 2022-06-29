using Model;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UserLogin.Controllers
{
    public class HomeController : Controller
    {
        Login_Interface loginService;
        public HomeController()
        {
            loginService = new Login_Service();
        }
        public ActionResult Index()
        {
            if (Session["UserDetails"] != null)
            {
                ViewBag.Breadcrum = "Dashboard";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ForgetPass()
        {
            ViewBag.Breadcrum = "User Details";
            return View("ForgetPass");
        }

        //Reset password

        [HttpPost]
        public ActionResult ResetPassword(Login_Model model, string NewPassword, string RePassword, string em)
        {
            if (model.Password != null && NewPassword != null && RePassword != null)
            {
                var userData = loginService.GetUserByEmail(em);
                var oldPass = userData.Password;
                var salt = userData.Username + em;
                model.LoginId = userData.LoginId;
                if (oldPass == Crypto.SHA1(model.Password + salt))
                {
                    Regex re = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                    var rex = re.IsMatch(NewPassword);
                    if (!rex)
                    {
                        Session["error"] = "New Password must be at least 8-15 characters long and contain 1 uppercase, 1 lowercase, 1 number and 1 special character.!!";
                        return RedirectToAction("ForgetPass", "Home", model);
                    }
                    else
                    {
                        if (NewPassword == RePassword)
                        {
                            model.Password = Crypto.SHA1(NewPassword + salt);
                            if (oldPass == model.Password)
                            {
                                Session["error"] = "New password cannot be as old password!!";
                                return RedirectToAction("ForgetPass", "Home", model);
                            }
                            else
                            {
                                if (loginService.Update(model))
                                {
                                    Session["success"] = "Passoword changed succefully.";
                                    return RedirectToAction("ForgetPass", "Home", model);
                                }
                                else
                                {
                                    Session["error"] = "Error changing password!!";
                                    return RedirectToAction("ForgetPass", "Home", model);
                                }
                            }
                        }
                        else
                        {
                            Session["error"] = "Password didn't matched!!";
                            return RedirectToAction("ForgetPass", "Home", model);
                        }
                    }

                }
                else
                {
                    Session["error"] = "Current password didn't matched!!";
                    return RedirectToAction("ForgetPass", "Home", model);
                }
            }
            else
            {
                Session["error"] = "Password can't be empty!!";
                return RedirectToAction("ForgetPass", "Home", model);
            }


        }
    }
}