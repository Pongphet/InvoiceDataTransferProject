﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceDataTransferred.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Search()
        {
            List<string> criteriaList = new List<string>();
            criteriaList.Add("Currency");
            criteriaList.Add("Date Range");
            criteriaList.Add("Status");
            ViewBag.Criteria = criteriaList;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}