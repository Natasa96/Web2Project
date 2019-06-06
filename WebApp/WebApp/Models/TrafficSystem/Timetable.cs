using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.TrafficSystem
{
    public class Timetable
    {
        private DateTime? day;
        private List<NetworkLine> lines;
        private List<DateTime> departures;
        private int id;

        public Timetable()
        {
            Departures = new List<DateTime>();
            Lines = new List<NetworkLine>();
        }

        public DateTime? Day { get => day; set => day = value; }
        public List<NetworkLine> Lines { get => lines; set => lines = value; }
        public List<DateTime> Departures { get => departures; set => departures = value; }
        public int Id { get => id; set => id = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}