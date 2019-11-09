using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Helpers;
using WebApp.Models;
using WebApp.Models.TrafficSystem;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Hubs
{
    /**
     * Prilikom pristizanja svake poruke instancira se novi Hub koji
     * je obradjuje.
     * **/
    [HubName("notifications")]
    public class SimulationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SimulationHub>();

        private static Timer timer = new Timer();
        private static int index = 0;
        private static double Lat {get;set;}
        private static double Lng { get; set; }
        private static List<Station> Stations { get; set; }
        private static object locker = new object();

        public SimulationHub()
        {
        }

        public void SetNetworkLine(List<Station> stations)
        {
            Stations = stations;
            Lat = Stations[0].Latitude;
            Lng = Stations[0].Longitude;
        }

        public void GetBusLocation()
        {
            lock (locker)
            { 
                //Svim klijentima se salje setRealTime poruka
                if(Stations != null)
                {
                    Coords coord = new Coords();
                    coord.longitude = Lng;
                    coord.latitude = Lat;
                    Clients.All.getBusLocation(coord);
                    index++;
                    if (index < Stations.Count)
                    {
                        Lat = Stations[index].Latitude;
                        Lng = Stations[index].Longitude;
                    }
                    else
                    {
                        index = 0;
                    }
                }
            }
        }

        public void TimeServerUpdates()
        {
            timer.Interval = 5000;
            timer.Start();
            timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            GetBusLocation();
        }

        public void StopTimeServerUpdates()
        {
            timer.Stop();
            this.Groups.Remove(this.Context.ConnectionId, "Admins");
        }

        public void NotifyAdmins(int clickCount)
        {
            hubContext.Clients.Group("Admins").userClicked($"Clicks: {clickCount}");
        }

        public override Task OnConnected()
        {
            if (Context.User.IsInRole("Admin"))
            {
                Groups.Add(Context.ConnectionId, "Admins");
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (Context.User.IsInRole("Admin"))
            {
                Groups.Remove(Context.ConnectionId, "Admins");
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}