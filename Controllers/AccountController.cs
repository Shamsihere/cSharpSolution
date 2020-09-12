using RentLentDemo.DataSets;
using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using static DataLibrary.BusinessLogic.SignupProcessor;
using static DataLibrary.BusinessLogic.PropertyProccessor;

namespace RentLentDemo.Controllers
{
    public class AccountController : Controller
    {
        NRentLentDBEntities db = new NRentLentDBEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Users login)
        {
            if(ModelState.IsValid)
            {
                var cred = db.SignupUserRoles.Where(model => model.Username == login.Username && model.Password == login.Password).FirstOrDefault();
                if (cred == null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(login.Username, true);
                    Session["username"] = login.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Models.Users login)
        {
            if (ModelState.IsValid)
            {
                long ContactNumber = login.ContactNumber;
                string Username = login.Username;
                var d = new
                {
                    ContactNumber,Username
                };
                var cred = GetForgotPassword(d);
                if (cred == null)
                {
                    ViewBag.ErrorMessage = "User doesn't Exists.";
                    return View();
                }
                else
                {
                    
                    ViewBag.PasswordHeading = "Your Password";
                    foreach (var item in cred)
                    {
                        ViewBag.Password = item.Password;
                    }
                    return View();
                }
            }
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Signup(SignupModel signup)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var data = new
                    {
                        signup.FirstName,
                        signup.LastName,
                        signup.DateOfBirth,
                        signup.Gender,
                        signup.Occupation,
                        signup.ContactNumber,
                        signup.EmailAddress,
                        signup.Address,
                        signup.City,
                        signup.NIC
                    };
                    var retrieve = SignupUser(data);
                    foreach (var item in retrieve)
                    {
                        TempData["username"] = $"{item.FirstName}{item.SignupId}";
                        TempData["signupid"] = item.SignupId;
                    }
                    TempData["role"] = signup.Role;

                    UploadImage(signup.ImageFile, Convert.ToInt32(TempData["signupid"]));
                }
                catch (Exception)
                {

                    throw;
                }
                
                return RedirectToActionPermanent("Password");
            }
            return View();
        }

        public void UploadImage(HttpPostedFileBase ImageFile, int UserId)
        {
            HttpPostedFileBase file = ImageFile;
            Random random = new Random();
            int randomNum = 0;
            List<string> listPath = new List<string>();

            string path = "-1";

            if (file != null)
            {
                randomNum = random.Next(10, 10000);
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") ||
                    extension.ToLower().Equals(".jpeg") ||
                    extension.ToLower().Equals(".png")
                    )
                {
                    try
                    {
                        path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/UserImages/"),
                        randomNum + "__" + Path.GetFileName(file.FileName).Replace(" ", ""));

                        //WebImage img = new WebImage(file.InputStream);
                        //img.Resize(600, 700, true, false);
                        //img.Crop(1, 1, 1, 1);
                        //img.Save(path);

                        file.SaveAs(path);

                        path = "~/Content/UserImages/" + randomNum + "__" + Path.GetFileName(file.FileName.Replace(" ", ""));

                        listPath.Add(path);

                    }
                    catch (Exception ex)
                    {

                        listPath = null;
                    }
                }
                else
                {
                    listPath.Add("~/Content/UserImages/profile.jpg");
                }
            }
            else
            {
                listPath.Add("~/Content/UserImages/profile.jpg");
            }
            UserImageUpload(UserId, listPath);
        }


        public ActionResult Password()
        {
            ViewBag.Username = TempData.Peek("username");
            ViewBag.SignupId = TempData.Peek("signupId");
            ViewBag.Role = TempData.Peek("role");
            return View();
        }

        [HttpPost]
        public ActionResult Password(PasswordModel pass)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Username = TempData["username"];
                ViewBag.SignupId = TempData["SignupId"];
                ViewBag.Role = TempData["role"];
                Convert.ToInt32(ViewBag.SignupId);
                var data = new
                {
                    ViewBag.SignupId,
                    ViewBag.Username,
                    pass.Password,
                    ViewBag.Role
                };

                SignupUserPassword(data);
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}