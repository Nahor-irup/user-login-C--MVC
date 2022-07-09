using Model;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserLogin.Controllers
{
    public class LoginController : Controller
    {
        Login_Interface loginService;
        public LoginController()
        {
            loginService = new Login_Service();
        }
        public ActionResult Index()
        {
            if (Session["UserDetails"] == null)
            {
                return View("SignIn");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOut()
        {
            Session["UserDetails"] = null;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        //to signup or register new user
        [HttpPost]
        public ActionResult SignupUser(Login_Model loginModel, string RePassword)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LeDmCYgAAAAAO8tmsQZvjtTdJ_uhe8x1zJZ7Alj";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            if (status)
            {
                if (ModelState.IsValid)
                {
                    if (loginModel.Password != RePassword)
                    {
                        Session["error"] = "Pasword not matched!!";
                        return View("SignUp");
                    }
                    else
                    {
                        if (loginService.UserExist(loginModel.Username))
                        {
                            Session["error"] = "Username already exist!!";
                            return View("SignUp");
                        }
                        else
                        {
                            if (loginService.EmailExist(loginModel.Email))
                            {
                                Session["error"] = "User with this email already exist!!";
                                return View("SignUp");
                            }
                            else
                            {
                                var salt = loginModel.Username + loginModel.Email;
                                loginModel.Password = Crypto.SHA1(loginModel.Password + salt);
                                loginModel.Attempt = 0;
                                if (loginService.Save(loginModel))
                                {
                                    Session["success"] = "User added succefully.";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    Session["success"] = "Error adding user!!";
                                    return View("Index");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Session["error"] = "Invalid data!!";
                    return View("SignUp", loginModel);
                }
            }
            else
            {

                Session["error"] = "Please verify captcha!!";
                return View("SignUp", loginModel);
            }
        }

        //for login
        [HttpPost]
        public ActionResult LoginUser(Login_Model model)
        {
            if (model == null)
            {
                Session["error"] = "Invalid user credential!!";
                return View("SignIn");
            }
            else
            {
                if (loginService.EmailExist(model.Email))
                {
                    var userData = loginService.GetUserByEmail(model.Email);
                    var getpass = userData.Password;  //retrive password stored in database
                    var getUsername = userData.Username; //Retrive username from databse
                    var hashPass = Crypto.SHA1(model.Password + getUsername + model.Email);  //hashing password to match password from database
                    if (hashPass != getpass)
                    {
                        if (userData.Attempt < 5)
                        {
                            Session["error"] = "Invalid user credential!!";
                            model.LoginId = userData.LoginId;
                            model.Attempt = userData.Attempt+1;
                            loginService.UpdateAttempt(model);
                            return View("SignIn");
                        }
                        else
                        {
                            Session["error"] = "User is banned for attempting more than 5 try!! Please contact system admin.";
                            return View("SignIn");
                        }

                    }
                    else
                    {
                        if (userData.Attempt >= 5)
                        {
                            Session["error"] = "User is banned for attempting more than 5 try!! Please contact system admin.";
                            return View("SignIn");
                        }
                        else
                        {
                            Session["UserDetails"] = loginService.GetUserByEmail(model.Email);
                            if (userData.Attempt ==0)
                            {
                                Session["Success"] = "Login Successful.";
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                model.LoginId = userData.LoginId;
                                model.Attempt = 0;
                                loginService.UpdateAttempt(model);
                                Session["Success"] = "Login Successful.";
                                return RedirectToAction("Index", "Home");
                            }
                            
                        }
                        
                    }
                }
                else
                {
                    Session["error"] = "Invalid user email!!";
                    return View("SignIn");
                }
            }
        }

    }
}