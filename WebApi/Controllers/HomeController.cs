using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi.Classes;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private InitializeData init = new InitializeData();
        public ActionResult Index()
        {
            // Inital data
            init.InitialStatusMappingData();
            init.InitialCurrencyCodeData();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
