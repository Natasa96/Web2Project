using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;
using WebApp.Models.Users;

namespace WebApp.Models.TicketService
{
    public class Ticket
    {
        private int id;
        private decimal price;
        private decimal discount;
        private TicketType type;
        private DateTime? validationTime;  //vreme kada je karta kupljena/cekirana

        public Ticket() { }

        public int Id { get => id; set => id = value; }
        public decimal Price { get => price; set => price = value; }
        public TicketType Type { get => type; set => type = value; }
        public DateTime? ValidationTime { get => validationTime; set => validationTime = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public Passenger Passenger { get; set; }
        public decimal DiscountPrice { get => price - price * (discount / 100); }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}