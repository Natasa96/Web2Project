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
        
        [Route("FullLineInfo/{id}")]
        [HttpGet]
        public IHttpActionResult GetFullInfo(int id)
        {
            try
            {
                EditLineInfoModel model = new EditLineInfoModel();
                NetworkLine line = UnitOfWork.NetworkLines.Find(x => x.LineNumber == id).First();
                model.LineNumber = line.LineNumber;
                var departures = UnitOfWork.Departures.GetAll();
                List<DeparturesViewModel> newmodel = new List<DeparturesViewModel>();
                foreach (var node in departures)
                    newmodel.Add(new DeparturesViewModel() {
                        Id = node.Id,
                        Time = node.Time
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
            NetworkLine networkLine = AdaptNetworkLine(model);

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
                UnitOfWork.NetworkLines.Update(AdaptNetworkLine(model));
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

        private NetworkLine AdaptNetworkLine(NetworkLineViewModel model)
        {
            NetworkLine networkLine = new NetworkLine();
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
                UnitOfWork.Stations.Update(station);
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
        public IHttpActionResult UpdateStation(Station model)
        {
            try
            {
                UnitOfWork.Stations.Update(model);
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

        private static StationViewModel AdaptStationViewModel(Station station)
        {
            StationViewModel model = new StationViewModel();

            model.Name = station.Name;
            model.Longitude = station.Longitude;
            model.Latitude = station.Latitude;
            model.Address = station.Address;

            if (station.NLine == null)
                station.NLine = new List<NetworkLine>();

            foreach (var line in station.NLine)
                model.NLine.Add(line.LineNumber);

            return model;
        }

        #endregion

        #region Timetabe

        [Route("GetTimetable")]
        [HttpGet]
        public IHttpActionResult GetTimetable()
        {
            try
            {
                return Ok(UnitOfWork.Timetables.GetAll());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("AddTimetable")]
        [HttpPost]
        public IHttpActionResult AddTimetable(TimetableViewModel model)
        {
            try
            {
                Timetable timetable = AdaptTimetable(model);

                foreach(var t in timetable.Lines)
                {
                    UnitOfWork.NetworkLines.Find(x => x.Id == t.Id).First().TimeOfGoing = timetable;
                }

                //model.Lines.Add(UnitOfWork.NetworkLines.Get(1));

                UnitOfWork.Timetables.Add(timetable);
                UnitOfWork.Complete();

                return Ok($"Timetable {timetable.Id} successfully added.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("UpdateTimetable")]
        [HttpPost]
        public IHttpActionResult UpdateTimetable(Timetable model)
        {
            try
            {
                UnitOfWork.Timetables.Update(model);
                UnitOfWork.Complete();
                return Ok($"Timetable {model.Id} successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteTimetable")]
        [HttpPost]
        public IHttpActionResult DeleteTimetable(Timetable model)
        {
            try
            {
                UnitOfWork.Timetables.Remove(model);
                UnitOfWork.Complete();
                return Ok($"Timetable {model.Id} deleted.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public TimetableViewModel AdaptTimetableViewModel(Timetable timetable)
        {
            TimetableViewModel model = new TimetableViewModel();

            model.Day = timetable.TTDay.ToString();
            
            foreach(var t in timetable.Lines)
            {
                model.NLine.Add(t.LineNumber);
            }

            return model;
        }

        public Timetable AdaptTimetable(TimetableViewModel model)
        {
            Timetable timetable = new Timetable();

            switch (model.Day)
            {
                case "RadniDan":
                    {
                        timetable.TTDay = TimetableType.RadniDan;
                        break;
                    }
                case "Praznik":
                    {
                        timetable.TTDay = TimetableType.Praznik;
                        break;
                    }
                case "Vikend":
                    {
                        timetable.TTDay = TimetableType.Vikend;
                        break;
                    }
            }

            if (model.NLine == null)
                model.NLine = new List<int>();

            foreach(var nl in model.NLine)
            {
                timetable.Lines.Add(UnitOfWork.NetworkLines.Find(x => x.LineNumber == nl).First());
            }

            return timetable;
        }

        #endregion

        #region Pricelist

        [Route("GetPricelist")]
        [HttpGet]
        public IHttpActionResult GetPricelist()
        {
            try
            {
                return Ok(UnitOfWork.Pricelist.GetAll());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("UpdatePricelist")]
        [HttpPost]
        public IHttpActionResult UpdatePricelist(Pricelist model)
        {
            try
            {
                UnitOfWork.Pricelist.Update(model);
                UnitOfWork.Complete();
                return Ok($"Pricelist {model.Id} successfully updated.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
