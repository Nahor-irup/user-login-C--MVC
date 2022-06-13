using Model;
using Services.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UserLogin.Controllers
{
    public class LoginController : Controller
    {
        Login_Interface loginService;
        public LoginController()
        {
            loginService = new Login_Service();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View("SignIn");
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        //to signup or register new user
        [HttpPost]
        public ActionResult SignupUser(Login_Model loginModel,string captcha, string RePassword)
        { 
            if (ModelState.IsValid)
            {
                if (captcha != null)
                { 
                    if(loginModel.Password!= RePassword)
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
                                var salt = loginModel.Username;
                                loginModel.Password = Crypto.SHA1(loginModel.Password + salt);
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
                    Session["error"] = "Invalid captcha!!";
                    return View("SignUp", loginModel);
                }
            }
            else
            {
                return View("SignUp",loginModel);
            }
        }

        //for login
        [HttpPost]
        public ActionResult LoginUser(Login_Model model)
        {
            if(model==null)
            {
                Session["error"] = "Invalid user credential!!";
                return View("SignIn");
            }
            else
            {
                if (loginService.EmailExist(model.Email))
                {
                    var getpass = loginService.GetUserByEmail(model.Email).Password;  //retrive password stored in database
                    var getUsername = loginService.GetUserByEmail(model.Email).Username; //Retrive username from databse
                    var hashPass = Crypto.SHA1(model.Password+getUsername);  //hashing password to match password from database
                    if(hashPass !=getpass )
                    {
                        Session["error"] = "Invalid user credential!!";
                        return View("SignIn");
                    }
                    else
                    {
                        Session["UserDetails"] = loginService.GetUserByEmail(model.Email);
                        Session["Success"] = "Login Successful.";
                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    Session["error"] = "Invalid user credential!!";
                    return View("SignIn");
                }
            }
        }
    }
}