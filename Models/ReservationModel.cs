using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ReservationModel
    {
        public int ReservationId { get; set; }

        public int TenantId { get; set; }
        public int PropertyId { get; set; }
        public int LandlordId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}
