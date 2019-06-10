using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TicketService
{
    public class TicketPrice
    {
        private int id;
        private TicketType type;
        private decimal price;

        public TicketPrice() { }

        public TicketType Type { get => type; set => type = value; }
        public decimal Price { get => price; set => price = value; }
        public int Id { get => id; set => id = value; }
        public Pricelist Pricelist { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}