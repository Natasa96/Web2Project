using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TrafficSystem
{
    public class Timetable
    {
        private TimetableType days;
        private int id;

        public Timetable()
        {
            Lines = new List<NetworkLine>();
        }

        public TimetableType TTDay { get => days; set => days = value; }
        public virtual  ICollection<NetworkLine> Lines { get; set; }
        public int Id { get => id; set => id = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}