using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public int SignupId { get; set; }
        
        public string Role { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string NIC { get; set; }


        public string City { get; set; }

        public string Occupation { get; set; }

        public DateTime DateOfBirth { get; set; }

        
        public long ContactNumber { get; set; }

        public string ImagePath { get; set; }
    }
}
