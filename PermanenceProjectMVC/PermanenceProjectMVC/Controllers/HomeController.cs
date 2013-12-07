using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PermanenceProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        public ActionResult Donate()
        {
            
            return View("Donate");
        }
        public ActionResult Contact()
        {
            return View("Contact");
        }
        public ActionResult NewView()
        {
            return View("NewView");
        }
    }
}
