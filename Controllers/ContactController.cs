using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.PropertyProccessor;

namespace RentLentDemo.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Message(FormCollection form)
        {
            string Name = form["name"];
            string Email = form["email"];
            string Sub = form["subject"];
            string Message = form["message"];
            var d = new
            {
                Name,
                Email,
                Sub,
                Message
            };
            SendContactMessage(d);
            return RedirectToAction("Index");
        }
    }
}