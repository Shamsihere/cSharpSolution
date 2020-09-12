using Microsoft.Ajax.Utilities;
using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using static DataLibrary.BusinessLogic.PropertyProccessor;
using static DataLibrary.BusinessLogic.SignupProcessor;



namespace RentLentDemo.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PropertyGridView()
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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);
        }

        [HttpPost]
        public ActionResult PropertyGridView(FormCollection form)
        {
            var Location = form["Area"];
            var Type = form["PropertyType"];
            var Person = form["NumberOfPerson"];
            var Room = form["Bedroom"];
            List<DataLibrary.Models.PropertyModel> data = new List<DataLibrary.Models.PropertyModel>();
            if (Type.IsEmpty() && Person.IsEmpty() && Room.IsEmpty())
            {
                var d = new
                {
                    Location
                };
                data = GetPropertyByLoc(d);
            }
            else if (Person.IsEmpty() && Room.IsEmpty())
            {
                var d = new
                {
                    Location,
                    Type
                };
                data = GetPropertyByType(d);
            }
            else
            {
                var d = new
                {
                    Location,
                    Room,
                    Person,
                    Type
                };
                data = GetProperty(d);
            }

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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);

        }

        public ActionResult PropertyGridViewSearch(string Room = "1", string Person = "1", string Location = "a")
        {
            var data = LoadPropertyBySearch(Location, Room, Person);
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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);
        }

        public ActionResult SharedPropertyGrid()
        {
            var data = LoadSharedProperty();
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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    ImagePath = item.ImagePath
                });
            }
            return View(property);
        }
        [Authorize]
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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    LandlordId = userId
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
                    NIC = item.NIC,
                    ImagePath = item.ImagePath
                    
                });

            }
            PropertyViewModel model = new PropertyViewModel();
            model.property = property;
            model.landlord = landlord;
            model.images = images;


            return View(model);
        }

        [Authorize]
        public ActionResult SharedPropertySingle(int id = 2015)
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
                    Deposit = Math.Round(item.Deposit, 2),
                    Rules = item.Rules,
                    LandlordId = userId
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
                    NIC = item.NIC,
                    ImagePath = item.ImagePath
                    
                });

            }
            PropertyViewModel model = new PropertyViewModel();
            model.property = property;
            model.landlord = landlord;
            model.images = images;


            return View(model);
        }

        [Authorize]
        public ActionResult Tenant(int PropertyId = 2016)
        {
            int RequestId = GetLandlordId(User.Identity.Name);
            var data = new
            {
                RequestId,
                PropertyId
            };
            var tenantIds = SharedProfile(data);

            List<SignupModel> tenant = new List<SignupModel>();
            for (int i = 0; i < tenantIds.Count; i++)
            {

                var user = GetTenant(Convert.ToInt32(tenantIds[i]));
                foreach (var item in user)
                {
                    tenant.Add(new SignupModel
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Gender = item.Gender,
                        DateOfBirth = item.DateOfBirth.ToShortDateString(),
                        ContactNumber = item.ContactNumber,
                        EmailAddress = item.EmailAddress,
                        Occupation = item.Occupation,
                        Address = item.Address,
                        City = item.City,
                        NIC = item.NIC,
                        ImagePath = item.ImagePath,
                        TenantId = RequestId,
                        SignupId = Convert.ToInt32(tenantIds[i])
                    });

                }
            }

            return View(tenant);

        }
        [Authorize]
        public ActionResult TenantSingle(int tenantId = 1013, int id = 1005)
        {
            int TenantId = tenantId;
            var user = GetTenant(id);
            List<SignupModel> signup = new List<SignupModel>();
            foreach (var item in user)
            {
                signup.Add(new SignupModel
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender,
                    DateOfBirth = item.DateOfBirth.ToShortDateString(),
                    ContactNumber = item.ContactNumber,
                    EmailAddress = item.EmailAddress,
                    Occupation = item.Occupation,
                    Address = item.Address,
                    City = item.City,
                    NIC = item.NIC,
                    ImagePath = item.ImagePath,
                    TenantId = tenantId,
                    SignupId = id
                });
            }
            return View(signup);
        }
    }
}