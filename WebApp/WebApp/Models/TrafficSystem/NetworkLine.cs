using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TrafficSystem
{
    public class NetworkLine
    {
        private int id;
        private int lineNumber;
        private LineType type;
        private Timetable timeOfGoing;

        public NetworkLine()
        {
            Stations = new List<Station>();
            Departures = new List<Departures>();
            ScheduleDays = new List<Schedule>();
        }

        public int Id { get => id; set => id = value; }
        public int LineNumber { get => lineNumber; set => lineNumber = value; }        
        public virtual ICollection<Station> Stations { get; set; }
        public LineType Type { get => type; set => type = value; }
        public virtual ICollection<Schedule> ScheduleDays { get ; set; }
        public virtual ICollection<Departures> Departures { get; set; }
        public Timetable TimeOfGoing { get => timeOfGoing; set => timeOfGoing = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}