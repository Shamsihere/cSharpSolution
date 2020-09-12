using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PropertyModel
    {
        public int PropertyId { get; set; }

        public int LandlordId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyAddress { get; set; }

        public string Measurement { get; set; }

        public string NumberOfPerson { get; set; }
        public string Bedroom { get; set; }
        public string Bathroom { get; set; }
        public string PropertyDiscription { get; set; }
        public double RentAmount { get; set; }
        public string RentType { get; set; }

        public string Contract { get; set; }
        public string Bills { get; set; }

        public string CancellationPolicy { get; set; }
        public double Deposit { get; set; }
        public string Rules { get; set; }

        public string ImagePath { get; set; }
    }

    public class Image
    {
        public string ImagePath { get; set; }
    }

}
