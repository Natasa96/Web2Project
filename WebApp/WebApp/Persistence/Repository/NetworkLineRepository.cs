using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.TrafficSystem;

namespace WebApp.Persistence.Repository
{
    public class NetworkLineRepository : Repository<NetworkLine, int>, INetworkLineRepository
    {
        public NetworkLineRepository(DbContext context) : base(context) { } 


    }
}