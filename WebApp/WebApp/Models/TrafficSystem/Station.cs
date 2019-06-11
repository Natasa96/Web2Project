
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models.TrafficSystem
{
    public class Station
    {
        private int id;
        private string name;
        private string address;
        private double longitude;
        private double latitude;

        public Station() : base()
        {
            NLine = new List<NetworkLine>();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        
        public virtual ICollection<NetworkLine>NLine { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public double Longitude { get => longitude; set => longitude = value; }
        public double Latitude { get => latitude; set => latitude = value; }
    }
}