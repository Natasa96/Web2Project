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

    //MOZDA CE SE MORATI PROSIRIVATI
    public class NetworkLineViewModel
    {
        public int LineNumber { get; set; }
        public string Type { get; set; }
        public List<int> Stations { get; set; }
        public List<DateTime> Departures { get; set; }

        public NetworkLineViewModel()
        {
            Stations = new List<int>();
            Departures = new List<DateTime>();
        }
    }

    public class StationViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<int> NLine { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public StationViewModel()
        {
            NLine = new List<int>();
        }
    }
}