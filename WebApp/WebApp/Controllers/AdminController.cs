using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Models.TicketService;
using WebApp.Models.TrafficSystem;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private DemoUnitOfWork _unitOfWork;

        public AdminController() { }

        public DemoUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
            private set
            {
                _unitOfWork = value;
            }
        }

        //Da li mi treba sobzirom da je vec na ../Admin stranici
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Route("AddLine")]
        [HttpPost]
        public IHttpActionResult AddLine(NetworkLine model)
        {
            UnitOfWork.NetworkLines.Add(model);
            UnitOfWork.Complete();
            return Ok($"Line {model.LineNumber} successfully added!");
        } 

        [Route("GetLines")]
        [HttpGet]
        public IHttpActionResult GetLines()
        {
            try
            {
                return Ok(UnitOfWork.NetworkLines.GetAll());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("GetStations")]
        [HttpGet]
        public IHttpActionResult GetStations()
        {
            try
            {
                return Ok(UnitOfWork.Stations.GetAll());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

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

        [Route("AddStation")]
        [HttpPost]
        public IHttpActionResult AddStation(Station model)
        {
            try
            {
                UnitOfWork.Stations.Add(model);
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

        [Route("AddTimetable")]
        [HttpPost]
        public IHttpActionResult AddTimetable(Timetable model)
        {
            try
            {
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
            catch(Exception ex)
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
            catch(Exception ex)
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
    }
}
