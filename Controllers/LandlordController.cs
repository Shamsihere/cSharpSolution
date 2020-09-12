
using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static DataLibrary.BusinessLogic.PropertyProccessor;
using static DataLibrary.BusinessLogic.SignupProcessor;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RentLentDemo.Controllers
{
    [Authorize(Roles = "Admin,Landlord")]
    public class LandlordController : Controller
    {

        public ActionResult AddProperty()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddProperty(PropertyModel property)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                int landlordId = GetLandlordId(username);
                var propertyTable = new { property.PropertyName, property.PropertyType, property.PropertyAddress, property.Bedroom, property.Bathroom, property.PropertyDiscription, property.NumberOfPerson, property.Measurement, landlordId };
                int propertyId = SaveProperty(propertyTable);
                var propertyInsert = new
                {
                    propertyId,
                    property.RentAmount,
                    property.RentType,
                    property.Contract,
                    property.Bills ,
                    property.CancellationPolicy,
                    property.Deposit,
                    property.Rules
                };
                InsertProperty(propertyInsert);
                UploadImage(property.ImageFile, propertyId);
                return RedirectToAction("LandlordProperty");
            }
            else
                return View();
        }
  
        public ActionResult LandlordProperty()
        {
            var username = User.Identity.Name;
            int landlordId = GetLandlordId(username);
            var data = LoadPropertyByLandlordId(landlordId);
            List<PropertyModel> property = new List<PropertyModel>();
            foreach (var item in data)
            {
                property.Add(new PropertyModel
                {
                    PropertyId = item.PropertyId,
                    PropertyName = item.PropertyName,
                    PropertyType = item.PropertyType,
                    PropertyAddress = item.PropertyAddress,
                    Measurement = item.Measurement,
                    NumberOfPerson = item.NumberOfPerson,
                    Bedroom = item.Bedroom,
                    Bathroom = item.Bathroom,
                    PropertyDiscription = item.PropertyDiscription,
                    RentAmount = item.RentAmount,
                    RentType = item.RentType,
                    Contract = item.Contract,
                    Bills = item.Bills,
                    CancellationPolicy = item.CancellationPolicy,
                    Deposit = item.Deposit,
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);
        }

        public ActionResult ViewProperty()
        {
            var data = LoadProperty();
            List<PropertyModel> property = new List<PropertyModel>();
            foreach (var item in data)
            {
                property.Add(new PropertyModel
                {
                    PropertyId = item.PropertyId,
                    PropertyName = item.PropertyName,
                    PropertyType = item.PropertyType,
                    PropertyAddress = item.PropertyAddress,
                    Measurement = item.Measurement,
                    NumberOfPerson = item.NumberOfPerson,
                    Bedroom = item.Bedroom,
                    Bathroom = item.Bathroom,
                    PropertyDiscription = item.PropertyDiscription,
                    RentAmount = item.RentAmount,
                    RentType = item.RentType,
                    Contract = item.Contract,
                    Bills = item.Bills,
                    CancellationPolicy = item.CancellationPolicy,
                    Deposit = item.Deposit,
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);
        }

        public ActionResult PropertySingle(int id = 2015)
        {
            List<PropertyModel> property = new List<PropertyModel>();
            List<SignupModel> landlord = new List<SignupModel>();
            List<Images> images = new List<Images>();

            var data = LoadPropertyById(id);

            int userId = LoadLandlordById(id);

            var collection = LoadUserById(userId);

            var img = LoadImageById(id);

            foreach (var item in data)
            {
                property.Add(new PropertyModel
                {
                    PropertyId = item.PropertyId,
                    PropertyName = item.PropertyName,
                    PropertyType = item.PropertyType,
                    PropertyAddress = item.PropertyAddress,
                    Measurement = item.Measurement,
                    NumberOfPerson = item.NumberOfPerson,
                    Bedroom = item.Bedroom,
                    Bathroom = item.Bathroom,
                    PropertyDiscription = item.PropertyDiscription,
                    RentAmount = item.RentAmount,
                    RentType = item.RentType,
                    Contract = item.Contract,
                    Bills = item.Bills,
                    CancellationPolicy = item.CancellationPolicy,
                    Deposit = item.Deposit,
                    Rules = item.Rules,
                    LandlordId = item.LandlordId
                });
            }

            foreach (var item in img)
            {
                images.Add(new Images
                {
                    ImagePath = item.ImagePath
                });
            }

            foreach (var item in collection)
            {
                landlord.Add(new SignupModel
                {
                    SignupId = item.SignupId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateOfBirth = item.DateOfBirth.ToShortDateString(),
                    Gender = item.Gender,
                    Occupation = item.Occupation,
                    ContactNumber = item.ContactNumber,
                    EmailAddress = item.EmailAddress,
                    Address = item.Address,
                    City = item.City,
                    NIC = item.NIC

                });

            }
            PropertyViewModel model = new PropertyViewModel();
            model.property = property;
            model.landlord = landlord;
            model.images = images;


            return View(model);
        }

        public ActionResult Shared(int PropertyId, string Shared)
        {
            if(Shared != null)
            {
                InsertShared(PropertyId, Shared);
            }
            return RedirectToAction("LandlordProperty");
        }

        public ActionResult Message()
        {
            int receiver = GetLandlordId(User.Identity.Name);
            List<MessageModel> messages = new List<MessageModel>();
            var data = GetLandlordMessages(receiver);
            foreach (var item in data)
            {
                messages.Add(new MessageModel
                {
                    MessageId = item.MessageId,
                    Message = item.Message,
                    Email = item.Email,
                    Name = item.Name,
                    TenantId = item.TenantId,
                    LandlordId = item.LandlordId,
                    UserName = GetLandlordName(item.TenantId)
                });
            }
            return View(messages);
        }

        [HttpPost]
        //public ActionResult Message(FormCollection form)
        //{
        //    int tenantId = Convert.ToInt32(form["tenantId"]);
        //    var data = new
        //    {
        //        tenantId,
        //        landlordId,
        //        Name,
        //        Email,
        //        Message
        //    };
        //    SendMessage(data);
        //    return View();
        //}
        //public ActionResult Message(int id, int landlordId, string Name, string Email, string Message)
        //{
        //    int tenantId = id;
        //    var data = new
        //    {   
        //        tenantId,
        //        landlordId,
        //        Name,
        //        Email,
        //        Message
        //    };
        //    SendMessage(data);
        //    return View();
        //}

        public ActionResult Confirmation(string Status, int ReservationId)
        {
            var data = new
            {
                ReservationId,
                Status
            };
            UpdateConfirmation(data);
            return RedirectToAction("Reservation");
        }

        public ActionResult Reservation()
        {
            List<Reservations> reservations = new List<Reservations>();
            int landlord = GetLandlordId(User.Identity.Name);
            var data = GetLandlordReservations(landlord);
            foreach (var item in data)
            {
                reservations.Add(new Reservations
                {
                    ReservationId = item.ReservationId,
                    TenantName = GetLandlordName( item.TenantId),
                    UserName = GetLandlordName( item.LandlordId),
                    TenantId = item.TenantId,
                    LandlordId = item.LandlordId,
                    PropertyId = item.PropertyId,
                    StartDate = item.StartDate.ToShortDateString(),
                    EndDate = item.EndDate.ToShortDateString(),
                    Status = item.Status
                });
            }
            return View(reservations);
        }

        public new ActionResult Profile()
        {

            var username = User.Identity.Name;
            var user = LoadUserByUsername(username);
            SignupModel signup = new SignupModel();
            foreach (var item in user)
            {

                signup.FirstName = item.FirstName;
                signup.LastName = item.LastName;
                signup.Gender = item.Gender;
                signup.Date = item.DateOfBirth.ToShortDateString();
                signup.ContactNumber = item.ContactNumber;
                signup.EmailAddress = item.EmailAddress;
                signup.Occupation = item.Occupation;
                signup.Address = item.Address;
                signup.City = item.City;
                signup.NIC = item.NIC;
                signup.ImagePath = item.ImagePath;
            }
            return View(signup);
        }

        
        public ActionResult DeleteProperty(int Id)
        {
            DeletePropertyy(Id);
            return RedirectToAction("LandlordProperty");
        }

        private void UploadImage(List<HttpPostedFileBase> imagePath, int propertyId)
        {
            List<HttpPostedFileBase> file = imagePath;
            Random random = new Random();
            int randomNum;
            List<string> listPath = new List<string>();
            
            string path;

            for (int i = 0; i < file.Count; i++)
            {

                    randomNum = random.Next(10, 10000);

                    string extension = Path.GetExtension(file[i].FileName);
                    if (extension.ToLower().Equals(".jpg") ||
                        extension.ToLower().Equals(".jpeg") ||
                        extension.ToLower().Equals(".png")
                        )
                    {
                       
                            path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PropertyImages/"),
                            randomNum + "__" + Path.GetFileName(file[i].FileName).Replace(" ", ""));

                            //WebImage img = new WebImage(file[i].InputStream);
                            //img.Resize(600, 700, true, false);

                            //img.Save(path);

                            
                            file[i].SaveAs(path);

                            

                            path = "~/Content/PropertyImages/" + randomNum + "__" + Path.GetFileName(file[i].FileName.Replace(" ", ""));

                            listPath.Add(path);

                        
                    }
                    else
                    {
                        listPath.Add("-2");
                    }
               
            }

            ImageUpload(propertyId, listPath);
        }


    }

}