using RentLentDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static DataLibrary.BusinessLogic.PropertyProccessor;
using static DataLibrary.BusinessLogic.SignupProcessor;

namespace RentLentDemo.Controllers
{
    
    public class TenantController : Controller
    {
        [Authorize(Roles = "Tenant,Admin")]

        // GET: Tenant
        [HttpPost]
        public ActionResult Delete(int id)
        {
            DeleteReservation(id);
            return RedirectToAction("Reservation");
        }
        [Authorize(Roles = "Tenant")]
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
                //signup.Add(new SignupModel
                //{
                //    FirstName = item.FirstName,
                //    LastName = item.LastName,
                //    Gender = item.Gender,
                //    DateOfBirth = item.DateOfBirth.ToShortDateString(),
                //    ContactNumber = item.ContactNumber,
                //    EmailAddress = item.EmailAddress,
                //    Occupation = item.Occupation,
                //    Address = item.Address,
                //    City = item.City,
                //    NIC = item.NIC,
                //    ImagePath = item.ImagePath
                //});
            }
            return View(signup);
        }

        public ActionResult Message()
        {
            int tenantId = GetLandlordId(User.Identity.Name);
            List<MessageModel> messages = new List<MessageModel>();
            var data = GetTenantMessages(tenantId);
            
            foreach (var item in data)
            {
                messages.Add(new MessageModel
                {
                    MessageId  = item.MessageId,
                    Message = item.Message,
                    Email = item.Email,
                    Name = item.Name,
                    UserName = GetLandlordName(item.TenantId),
                    TenantId = tenantId,
                    LandlordName = GetLandlordName(item.TenantId).ToString()
            });
            }
            
            return View(messages);
        }

        
        [HttpPost]
        public ActionResult Message(int id,int landlordId, string Name, string Email, string Message)
        {
            if(ModelState.IsValid)
            {
                int tenantId = GetLandlordId(User.Identity.Name);
                var data = new
                {
                    tenantId,
                    landlordId,
                    Name,
                    Email,
                    Message
                };
                SendMessage(data);
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }

        [Authorize(Roles = "Tenant,Admin")]
        public ActionResult Reservation()
        {
            List<Reservations> reservations = new List<Reservations>();
            int tenantId = GetLandlordId(User.Identity.Name);
            var data = GetReservations(tenantId);
            foreach (var item in data)
            {
                reservations.Add(new Reservations
                {
                    ReservationId = item.ReservationId,
                    TenantName = GetLandlordName( item.TenantId),
                    UserName = GetLandlordName(item.LandlordId),
                    PropertyId = item.PropertyId,
                    StartDate = item.StartDate.ToShortDateString(),
                    EndDate = item.EndDate.ToShortDateString(),
                    Status = item.Status,
                    TenantId = item.TenantId,
                    LandlordId = item.LandlordId
                });
            }
            return View(reservations);
        }

        [Authorize(Roles = "Tenant,Admin")]
        [HttpPost]
        public ActionResult Reservation(Reservations res)
        {
            if(ModelState.IsValid)
            {
                var tenantId = GetLandlordId(User.Identity.Name);
                var landlordId = GetLandlordByPropertyId(res.PropertyId);

                var data = new
                {
                    res.StartDate,
                    res.EndDate,
                    tenantId,
                    landlordId,
                    res.PropertyId
                };
                SaveReservation(data);
                return RedirectToAction("Reservation");
            }
            return RedirectToAction("PropertyGridView", "Home");
            
        }

        public ActionResult Payment()
        {
            return View();
        }
    }
}