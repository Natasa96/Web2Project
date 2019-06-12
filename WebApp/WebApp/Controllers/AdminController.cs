using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Models.TicketService;
using WebApp.Models.TrafficSystem;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private IUnitOfWork UnitOfWork;
        private DbContext _conext;

        public AdminController(IUnitOfWork unitOfWork, DbContext context) {
            UnitOfWork = unitOfWork;
            _conext = context;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        #region NetworkLines

        [Route("GetLines")]
        [HttpGet]
        public IHttpActionResult GetLines()
        {
            try
            {
                List<NetworkLineViewModel> nl = new List<NetworkLineViewModel>(); 

                foreach(var line in UnitOfWork.NetworkLines.GetAll())
                {
                    nl.Add(AdaptNetworkLineViewModel(line));
                }

                return Ok(nl);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [Route("GetFullStationInfo/{id}")]
        [HttpGet]
        public IHttpActionResult GetStationFullInfo(int id)
        {
            EditStationViewModel model = CreateEditStationViewModel(id);
            if (model == null)
                return BadRequest();
            return Ok(model);
        }

        private EditStationViewModel CreateEditStationViewModel(int id)
        {
            Station s = UnitOfWork.Stations.Get(id);
            EditStationViewModel model = new EditStationViewModel()
            {
                Id = s.Id,
                Address = s.Address,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                Name = s.Name
            };
            foreach (var node in s.NLine)
                model.SelectedLines.Add(node.LineNumber);
            foreach (var node in UnitOfWork.NetworkLines.GetAll())
                model.NLine.Add(node.LineNumber);
            return model;
        }

        [Route("FullLineInfo/{id}")]
        [HttpGet]
        public IHttpActionResult GetFullInfo(int id)
        {
            try
            {
                EditLineInfoModel model = new EditLineInfoModel();
                NetworkLine line = UnitOfWork.NetworkLines.Find(x => x.LineNumber == id).First();

                model.Id = line.Id;
                model.LineNumber = line.LineNumber;
                var departures = UnitOfWork.Departures.GetAll();
                List<DeparturesViewModel> newmodel = new List<DeparturesViewModel>();
                foreach (var node in departures)
                    newmodel.Add(new DeparturesViewModel() {
                        Id = node.Id,
                        Time = node.Time.ToUniversalTime()
                    });
                model.Departures = newmodel;
                model.SelectedType = line.Type.ToString();
                model.AllTypes = Enum.GetNames(typeof(LineType)).ToList();
                foreach (var node in line.Stations)
                    model.SelectedStations.Add(AdaptStationViewModel(node));
                foreach (var node in UnitOfWork.Stations.GetAll())
                    model.AllStations.Add(AdaptStationViewModel(node));
                foreach (var node in line.ScheduleDays)
                    model.SelectedSchedule.Add(node.Type.ToString());
                model.AllSchedule = Enum.GetNames(typeof(TimetableType)).ToList();
                return Ok(model);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("AddLine")]
        [HttpPost]
        public IHttpActionResult AddLine(NetworkLineViewModel model)
        {
            NetworkLine networkLine = AdaptNetworkLine(model, 0);

            UnitOfWork.NetworkLines.Add(networkLine);
            UnitOfWork.Complete();

            return Ok($"Line {model.LineNumber} successfully added!");
        }

        [Route("GetLines/id")]
        [HttpGet]
        public IHttpActionResult GetStationsForLine(int id)
        {
            try
            {
                return Ok(UnitOfWork.NetworkLines.Find(x => x.Id == id).First().Stations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("UpdateLine")]
        [HttpPost]
        public IHttpActionResult UpdateLine(NetworkLineViewModel model)
        {
            try
            {
                NetworkLine line = AdaptNetworkLine(model, 1);
 
                UnitOfWork.NetworkLines.Update(line);
                UnitOfWork.Complete();
                return Ok($"Line {model.LineNumber} successfully updated!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("DeleteLine")]
        [HttpPost]
        public IHttpActionResult DeleteLine(NetworkLine model)
        {
            try
            {
                UnitOfWork.NetworkLines.Remove(model);
                UnitOfWork.Complete();
                return Ok($"Line {model.LineNumber} deleted!");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //COMMAND - add == 0, update == 1
        private NetworkLine AdaptNetworkLine(NetworkLineViewModel model, int command)
        {

            NetworkLine networkLine;

            if (command == 0)
                networkLine = new NetworkLine();
            else
            {
                networkLine = UnitOfWork.NetworkLines.Get(model.Id);

                int cntDepartures = networkLine.Departures.Count;
                for (int item = 0; item < cntDepartures; item++)
                {
                    UnitOfWork.Departures.Remove(networkLine.Departures.ToList()[0]);

                }

                int cntSchedule = networkLine.ScheduleDays.Count;
                for (int item = 0; item < cntSchedule; item++)
                {
                    UnitOfWork.Schedules.Remove(networkLine.ScheduleDays.ToList()[0]);
                    //networkLine.ScheduleDays.Remove(networkLine.ScheduleDays.ToList()[item]);
                }

                int cntStations = networkLine.Stations.Count;
                for (int item = 0; item < cntStations; item++)
                {
                    var station = networkLine.Stations.ToList()[0];
                    station.NLine.Remove(networkLine);
                    networkLine.Stations.Remove(station);
                }

                UnitOfWork.Complete();
            }

            if (UnitOfWork.NetworkLines.GetAll().ToList().Exists(x => x.LineNumber == model.LineNumber))
                return null;

            networkLine.LineNumber = model.LineNumber;

            switch (model.Type)
            {
                case "Gradska":
                    {
                        networkLine.Type = LineType.Gradska;
                        break;
                    }
                case "Prigradska":
                    {
                        networkLine.Type = LineType.Prigradska;
                        break;
                    }
                default:
                    {
                        networkLine.Type = LineType.Gradska;
                        break;
                    }
            }

            if (model.Departures == null)
                model.Departures = new List<DateTime>();

            foreach (var d in model.Departures)
            {
                networkLine.Departures.Add(new Departures() { Time = d, NetworkLine=networkLine});
            }

            if (model.Stations == null)
                model.Stations = new List<int>();

            foreach (var s in model.Stations) 
            {
                var station = UnitOfWork.Stations.Find(
                    x => x.Name == s.ToString()).First();
                station.NLine.Add(networkLine);
                networkLine.Stations.Add(station);
            }

            if (model.ScheduleDays == null)
                model.ScheduleDays = new List<string>();

            foreach (var s in model.ScheduleDays)
            {
                if (s == "RadniDan")
                    networkLine.ScheduleDays.Add(new Schedule() { Type = TimetableType.RadniDan, NetworkLine=networkLine });
                else if (s == "Praznik")
                    networkLine.ScheduleDays.Add(new Schedule() { Type = TimetableType.Praznik, NetworkLine = networkLine });
                else if (s == "Vikend")
                    networkLine.ScheduleDays.Add(new Schedule() { Type = TimetableType.Vikend, NetworkLine = networkLine });
            }

            return networkLine;
        }

        private static NetworkLineViewModel AdaptNetworkLineViewModel(NetworkLine line)
        {
            NetworkLineViewModel model = new NetworkLineViewModel();
            model.LineNumber = line.LineNumber;

            switch (model.Type)
            {
                case "Gradska":
                    {
                        model.Type = "Gradska";
                        break;
                    }
                case "Prigradska":
                    {
                        model.Type = "Prigradska";
                        break;
                    }
                default:
                    {
                        model.Type = "Gradska";
                        break;
                    }
            }

            if (line.Departures == null)
                line.Departures = new List<Departures>();

            foreach (var d in line.Departures)
            {
                model.Departures.Add(d.Time);
            }

            if (line.Stations == null)
                line.Stations = new List<Station>();

            foreach(var s in line.Stations)
            {
                model.Stations.Add(Convert.ToInt32(s.Name));
            }

            if (line.ScheduleDays == null)
                line.ScheduleDays = new List<Schedule>();

            foreach(var s in line.ScheduleDays)
            {
                model.ScheduleDays.Add(s.ToString());
            }

            return model;
        }

        #endregion

        #region Stations

        [Route("GetStations")]
        [HttpGet]
        public IHttpActionResult GetStations()
        {
            try
            {
                List<StationViewModel> stations = new List<StationViewModel>();

                foreach (var s in UnitOfWork.Stations.GetAll())
                    stations.Add(AdaptStationViewModel(s));

                return Ok(stations);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("AddStation")]
        [HttpPost]
        public IHttpActionResult AddStation(StationViewModel model)
        {
            try
            {
                Station station = new Station();

                station.Address = model.Address;
                station.Latitude = model.Latitude;
                station.Longitude = model.Longitude;
                station.Name = model.Name;

                if(model.NLine == null)
                    model.NLine = new List<int>();

                foreach (var line in model.NLine)
                {
                    station.NLine.Add(UnitOfWork.NetworkLines.Find(x => x.LineNumber == line).First());
                    var networkline = UnitOfWork.NetworkLines.Find(x => x.LineNumber == line).First();
                    networkline.Stations.Add(station);
                    UnitOfWork.NetworkLines.Update(networkline);
                }
                UnitOfWork.Stations.Add(station);
                UnitOfWork.Complete();
                return Ok($"Station {model.Name} successfully added.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("UpdateStation")]
        [HttpPost]
        public IHttpActionResult UpdateStation(StationViewModel model)
        {
            try
            {
                UnitOfWork.Stations.Update(AdaptStation(model));
                UnitOfWork.Complete();
                return Ok($"Station {model.Name} successfully updated.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("DeleteStation")]
        [HttpPost]
        public IHttpActionResult DeleteStation(Station model)
        {
            try
            {
                UnitOfWork.Stations.Remove(model);
                UnitOfWork.Complete();
                return Ok($"Station {model.Name} deleted.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private Station AdaptStation(StationViewModel model)
        {
            Station s = UnitOfWork.Stations.Get(model.Id);
            s.Name = model.Name;
            s.Address = model.Address;
            s.Latitude = model.Latitude;
            s.Longitude = model.Longitude;
            int cnt = s.NLine.Count;
            for(int i = 0; i < cnt; i++)
            {
                var line = s.NLine.ToList()[i];
                line.Stations.Remove(s);
                s.NLine.Remove(line);
            }
            UnitOfWork.Complete();

            foreach(var node in model.NLine)
            {
                s.NLine.Add(UnitOfWork.NetworkLines.Find(x => x.LineNumber == node).First());
                UnitOfWork.NetworkLines.Find(x => x.LineNumber == node).First().Stations.Add(s);
            }
            return s;
        }

        private static StationViewModel AdaptStationViewModel(Station station)
        {
            StationViewModel model = new StationViewModel();

            model.Name = station.Name;
            model.Longitude = station.Longitude;
            model.Latitude = station.Latitude;
            model.Address = station.Address;
            model.Id = station.Id;

            if (station.NLine == null)
                station.NLine = new List<NetworkLine>();

            foreach (var line in station.NLine)
                model.NLine.Add(line.LineNumber);

            return model;
        }

        #endregion

        #region Timetabe

        [Route("GetLinesSchedule/{type}")]
        [HttpGet]
        public IHttpActionResult GetTimetable(string type)
        {
            try
            {
                List<ScheduleNLineViewModel> model = new List<ScheduleNLineViewModel>();
                var schedules = UnitOfWork.Schedules.GetAll();
                var networkList = UnitOfWork.NetworkLines.GetAll();
                
                foreach(var line in networkList)
                {
                    foreach(var item in line.ScheduleDays)
                    {
                        if(item.Type.ToString() == type)
                            model.Add(new ScheduleNLineViewModel()
                            {
                                Id = item.NetworkLine.Id,
                                LineNumber = item.NetworkLine.LineNumber
                            });
                    }
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("GetDeparturesLine/{id}")]
        [HttpGet]
        public IHttpActionResult GetDepartures(int id)
        {
            try
            {
                List<DeparturesViewModel> dep = new List<DeparturesViewModel>();
                var lines = UnitOfWork.NetworkLines.Get(id);

                foreach(var item in lines.Departures)
                {
                    dep.Add(new DeparturesViewModel()
                    {
                        Id = item.Id,
                        Time = item.Time
                    });
                }

                return Ok(dep);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Route("UpdateTimetable")]
        [HttpPost]
        public IHttpActionResult UpdateTimetable(ScheduleDeparturesViewModel model)
        {
            try
            {
                AdaprtTimetable(model);
                return Ok($"Timetable {model.selectedNLine} successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void AdaprtTimetable(ScheduleDeparturesViewModel model)
        {
            var line = UnitOfWork.NetworkLines.Get(model.selectedNLine);
            var deps = UnitOfWork.Departures.GetAll().Where(x => x.NetworkLine.Id == line.Id).ToList();
            int cnt = deps.Count;
            for(int i = 0; i < cnt; i++)
            {
                UnitOfWork.Departures.Remove(line.Departures.ToList()[0]);
            }
            UnitOfWork.Complete();
            line.Departures.Clear();
            foreach(var item in model.Departures)
            {
                line.Departures.Add(new Departures()
                {
                    Time = DateTime.Parse(item),
                    NetworkLine = line
                });
            }
            UnitOfWork.NetworkLines.Update(line);
            UnitOfWork.Complete();
        }

        #endregion

        #region Pricelist

        [Route("GetPricelist")]
        [HttpGet]
        public IHttpActionResult GetPricelist()
        {
            try
            {
                return Ok(AdaptPricelistViewModel());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private PricelistViewModel AdaptPricelistViewModel()
        {
            PricelistViewModel model = new PricelistViewModel();
            model.TicketPrice = new Dictionary<string, int>();
            foreach(var item in UnitOfWork.TicketPrice.GetAll())
            {
                model.TicketPrice.Add(item.Type.ToString(), (int)item.Price);
            }
            return model;
        }

        [Route("UpdatePricelist")]
        [HttpPost]
        public IHttpActionResult UpdatePricelist(PricelistViewModel model)
        {
            try
            {
                Pricelist plist = UnitOfWork.Pricelist.Find(x => x.Active == true).First();
                plist.Active = false;
                plist.EndTime = DateTime.Today;
                UnitOfWork.Pricelist.Update(plist);
                Pricelist newplist = new Pricelist();
                newplist.Active = true;
                newplist.ActivePrices = plist.ActivePrices;
                var tPrice = UnitOfWork.TicketPrice.GetAll().ToList();
                for(int i =0; i < 4; i++)
                {
                    tPrice[i].Pricelist = newplist;
                    if(tPrice[i].Type == TicketType.Dnevna)
                    {
                        tPrice[i].Price = model.TicketPrice["Dnevna"];
                    }
                    else if(tPrice[i].Type == TicketType.Godisnja)
                    {
                        tPrice[i].Price = model.TicketPrice["Godisnja"];
                    }
                    else if(tPrice[i].Type == TicketType.Mesecna)
                    {
                        tPrice[i].Price = model.TicketPrice["Mesecna"];
                    }
                    else
                    {
                        tPrice[i].Price = model.TicketPrice["Vremenska"];
                    }
                }
                UnitOfWork.Complete();
                newplist.EndTime = null;
                newplist.StartTime = DateTime.Today;
                foreach (var node in UnitOfWork.TicketPrice.GetAll())
                    newplist.ActivePrices.Add(node);
                UnitOfWork.Pricelist.Add(newplist);
                UnitOfWork.Complete();
                return Ok($"Pricelist successfully updated.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
