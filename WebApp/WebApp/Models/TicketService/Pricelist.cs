using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TicketService
{
    public class Pricelist
    {
        private int id;
        private DateTime? startTime;
        private DateTime? endTime;
        private bool active;

        public Pricelist()
        {
            ActivePrices = new List<TicketPrice>();
        }


        public DateTime? StartTime { get => startTime; set => startTime = value; }
        public DateTime? EndTime { get => endTime; set => endTime = value; }
        public bool Active { get => active; set => active = value; }
        public virtual ICollection<TicketPrice> ActivePrices { get; set ; }
        public int Id { get => id; set => id = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}