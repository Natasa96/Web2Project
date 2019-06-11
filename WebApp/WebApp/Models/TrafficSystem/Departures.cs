using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.TrafficSystem
{
    public class Departures
    {
        private int id;
        private DateTime time;

        public Departures() { }

        public DateTime Time { get => time; set => time = value; }
        public int Id { get => id; set => id = value; }
        public NetworkLine NetworkLine { get; set; }
    }
}