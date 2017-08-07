using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThermostatApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Education()
        {
            ViewBag.Title = "Education";

            return View();
        }

        public ActionResult Work()
        {
            ViewBag.Title = "Work Experience";

            return View();
        }

        public ActionResult Skills()
        {
            ViewBag.Title = "Skills & Awards";

            return View();
        }

        public ActionResult Transcript()
        {
            ViewBag.Title = "Transcript";

            return View();
        }
    }
}
