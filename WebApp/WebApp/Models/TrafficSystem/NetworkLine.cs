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
        private List<Station> stations;
        private LineType type;

        public NetworkLine()
        {
            Stations = new List<Station>();
        }

        public int Id { get => id; set => id = value; }
        public int LineNumber { get => lineNumber; set => lineNumber = value; }

        [ForeignKey("StationsID")]
        public List<Station> Stations { get => stations; set => stations = value; }

        public LineType Type { get => type; set => type = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}