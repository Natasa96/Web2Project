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
        public IHttpActionResult UpdateLine(NetworkLine model)
        {
            try
            {
                UnitOfWork.NetworkLines.Update(model);
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

            foreach (var d in model.Departures)
            {
                networkLine.Departures.Add(new Departures() { Time = d });
            }

            foreach (var s in model.Stations) 
            {
                networkLine.Stations.Add(UnitOfWork.Stations.Find(
                    x => x.Name == s.ToString()).First());
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

            foreach (var d in model.Departures)
            {
                model.Departures.Add(d);
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

                foreach (var line in model.NLine)
                {
                    station.NLine.Add(UnitOfWork.NetworkLines.Find(x => x.LineNumber == line).First());
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
        public IHttpActionResult AddTimetable(Timetable model)
        {
            try
            {
                model.Lines.Add(UnitOfWork.NetworkLines.Get(1));

                UnitOfWork.Timetables.Add(model);
                UnitOfWork.Complete();

                return Ok($"Timetable {model.Id} successfully added.");
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
