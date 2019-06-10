using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class GetPriceViewModel
    {
        public string Type { get; set; }
    }

    public class TicketDataViewModel
    {
        public int Price { get; set; }
        public string Type { get; set; }
    }

    public class PassengerInfoViewModel
    {
        public PassengerInfoViewModel() { Types = new List<string>(); }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool Validation { get; set; }
        public string Document { get; set; }
        public string Type { get; set; }
        public List<string> Types {get;set;}
    }

    public class TicketPriceInfoViewModel
    {
        public TicketType TicketType { get; set; }
    }
}