using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.TicketService
{
    public class PaypalCredentials
    {
        public string Id { get; set; }
        public string PurchaseUnit { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Status { get; set; }

        public Ticket Ticket { get; set; }
    }
}