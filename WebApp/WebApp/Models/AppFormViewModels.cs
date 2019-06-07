using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class PassengerInfoViewModel
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Validation { get; set; }
        public string Document { get; set; }
    }

    public class TicketPriceInfoViewModel
    {
        public TicketType TicketType { get; set; }
    }
}