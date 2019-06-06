
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.TrafficSystem
{
    public class Station
    {
        private int id;
        private string name;
        private string address;
        private List<NetworkLine> nLine;
        private GeoCoord coord;

        public Station() : base()
        {
            NLine = new List<NetworkLine>();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public List<NetworkLine >NLine { get => nLine; set => nLine = value; }
        public GeoCoord Coordinate { get => coord; set => coord = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}