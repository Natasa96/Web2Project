using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TrafficSystem
{
    public class Schedule
    {
        private int id;
        private TimetableType type;

        public Schedule(){}

        public TimetableType Type { get => type; set => type = value; }
        public int Id { get => id; set => id = value; }
        public NetworkLine NetworkLine { get; set; }
    }
}