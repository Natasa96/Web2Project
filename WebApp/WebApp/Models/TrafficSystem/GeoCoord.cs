using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.TrafficSystem
{
    public class GeoCoord
    {
        private int id;
        private double latitude;
        private double longitude;

        public GeoCoord() { }


        public double Longitude { get => longitude; set => longitude = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public int Id { get => id; set => id = value; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}