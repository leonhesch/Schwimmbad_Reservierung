using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruppenreservierungen
{
    public class Reservation
    {
        public string GroupName { get; set; }
        public int GroupSize { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Requirements { get; set; }
    }

}
