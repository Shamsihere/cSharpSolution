using Microsoft.Ajax.Utilities;
using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;


namespace RentLentDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

}