using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.Models.TrafficSystem;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Sim")]
    public class SimController : ApiController
    {
        private SimulationHub hub;
        private IUnitOfWork UnitOfWork;
        private DbContext context;

        public SimController(SimulationHub hub, IUnitOfWork unit, DbContext context)
        {
            this.hub = hub;
            this.UnitOfWork = unit;
            this.context = context;
        }

        [HttpPost]
        [Route("SetNetworkLine")]
        //POST: api/SetNetworkLine
        public IHttpActionResult SetNetworkLine(ScheduleNLineViewModel model)
        {
            var data = UnitOfWork.NetworkLines.Find(x => x.Id == model.Id).First().Stations.ToList();
            hub.SetNetworkLine(data);
            List<Coords> retData = new List<Coords>();
            foreach(var node in data)
            {
                retData.Add(new Coords()
                {
                    latitude = node.Latitude,
                    longitude = node.Longitude
                });
            }
            return Ok(retData);
        }
        public static int ClickCount { get; set; }
        // GET: api/Click
        public IHttpActionResult Post()
        {
            hub.NotifyAdmins(++ClickCount);
            return Ok("Hello");
        }
    }
}
