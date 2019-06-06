using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.TrafficSystem;

namespace WebApp.Persistence.Repository
{
    public class GeoCoordRepository : Repository<GeoCoord, int>, IGeoCoordRepository
    {
        public GeoCoordRepository(DbContext context) : base(context) { }
    }
}