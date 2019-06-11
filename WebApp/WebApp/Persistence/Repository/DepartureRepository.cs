using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.TrafficSystem;
using WebApp.Persistence.Repository.Interfaces;

namespace WebApp.Persistence.Repository
{
    public class DepartureRepository : Repository<Departures,int>, IDepartureRepository
    {
        public DepartureRepository(DbContext context) : base(context) { }
    }
}