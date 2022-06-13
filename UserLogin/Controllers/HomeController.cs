using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserDetails"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
    }
}