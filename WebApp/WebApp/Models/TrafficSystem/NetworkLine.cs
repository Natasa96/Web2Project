using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TrafficSystem
{
    public class NetworkLine
    {
        private int id;
        private int lineNumber;
        private List<Station> stations;
        private List<GeoCoord> buses;
        private LineType type;

        public NetworkLine()
        {
            Stations = new List<Station>();
        }

        public int Id { get => id; set => id = value; }
        public int LineNumber { get => lineNumber; set => lineNumber = value; }
        public List<Station> Stations { get => stations; set => stations = value; }
        public LineType Type { get => type; set => type = value; }
        public List<GeoCoord> Buses { get => buses; set => buses = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}