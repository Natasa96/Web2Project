using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;
using WebApp.Models.TicketService;

namespace WebApp.Models.Users
{
    public class Passenger : ApplicationUser
    {
        private string document;
        private PassengerType type;
        private bool validation;
        private List<Ticket> tickets;

        public Passenger() : base()
        {
            tickets = new List<Ticket>();
        }

        public string Document { get => document; set => document = value; }
        public PassengerType Type { get => type; set => type = value; }
        public bool Validation { get => validation; set => validation = value; }
        public List<Ticket> Tickets { get => tickets; set => tickets = value; }
    }
}