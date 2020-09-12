using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static DataLibrary.BusinessLogic.SignupProcessor;
using static DataLibrary.BusinessLogic.PropertyProccessor;

namespace RentLentDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
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
                signup.DateOfBirth = item.DateOfBirth.ToShortDateString();
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

        public ActionResult Message()
        {
            
            List<MessageModel> messages = new List<MessageModel>();
            var data = GetAdminMessage();

            foreach (var item in data)
            {
                messages.Add(new MessageModel
                {
                   
                    Message = item.Message,
                    Email = item.Email,
                    Name = item.Name,
                    Subject = item.Subject
                    
                });
            }

            return View(messages);
        }

        public ActionResult Tenants()
        {
            List<SignupModel> tenant = new List<SignupModel>();
            var user = GetTenants();
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
                    SignupId = item.SignupId
                });

            }
            return View(tenant);
        }

        public ActionResult TenantView(int id)
        {
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
                    
                    SignupId = id
                });
            }
            return View(signup);
        }

        public ActionResult Landlords()
        {
            List<SignupModel> landlord = new List<SignupModel>();
            var user = GetLandlords();
            foreach (var item in user)
            {
                landlord.Add(new SignupModel
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
                    SignupId = item.SignupId
                });

            }
            return View(landlord);
        }

        public ActionResult LandlordView(int id)
        {
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
                    
                    SignupId = id
                });
            }
            return View(signup);
        }

        public ActionResult Delete(int id)
        {
            DeleteUser(id);
            var data = LoadPropertyByLandlordId(id);
            foreach (var item in data)
            {
                DeletePropertyy(item.PropertyId);
            }
            return RedirectToAction("Landlords");
        }

        public ActionResult DeleteTenant(int id)
        {
            DeleteUser(id);
            var data = GetReservations(id);
            foreach (var item in data)
            {
                DeleteReservation(item.ReservationId);
            }
            return RedirectToAction("Landlords");
        }
    }
}