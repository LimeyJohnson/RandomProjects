﻿using System.Web.Mvc;

namespace MvcDirectoryGraphSample.Controllers
{
    /// <summary>
    /// Controller for the Home page of the application.
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Windows Azure Active Directory Sample";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }        
    }
}
