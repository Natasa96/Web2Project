using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Enums")]
    public class EnumsController : ApiController
    {
        private IUnitOfWork UnitOfWork;
        private DbContext _conext;

        public EnumsController(IUnitOfWork unitOfWork, DbContext context)
        {
            UnitOfWork = unitOfWork;
            _conext = context;
        }

        [Route("GetLinesSchedule/{type}")]
        [HttpGet]
        public IHttpActionResult GetLinesSchedule(string type)
        {
            try
            {
                List<ScheduleNLineViewModel> model = new List<ScheduleNLineViewModel>();
                var schedules = UnitOfWork.Schedules.GetAll();
                var networkList = UnitOfWork.NetworkLines.GetAll();

                foreach (var line in networkList)
                {
                    foreach (var item in line.ScheduleDays)
                    {
                        if (item.Type.ToString() == type)
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

                foreach (var item in lines.Departures)
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


        [HttpGet]
        [Route("GetLineType")]
        [AllowAnonymous]
        //GET   api/Enums/GetLineType
        public IHttpActionResult GetLineTypes()
        {
            return Ok(Enum.GetNames(typeof(LineType)).ToList());
        }

        [HttpGet]
        [Route("GetPassangerType")]
        [AllowAnonymous]
        //GET   api/Enums/GetPassanger
        public IHttpActionResult GetPassangerTypes()
        {
            return Ok(Enum.GetNames(typeof(PassengerType)).ToList());
        }

        [HttpGet]
        [Route("GetTicketType")]
        [AllowAnonymous]
        //GET   api/Enums/GetTicketType
        public IHttpActionResult GetTicketTypes()
        {
            return Ok(Enum.GetNames(typeof(TicketType)).ToList());
        }

        //GET   api/Enums/GetTimetableType
        [HttpGet]
        [Route("GetSchedule")]
        public IHttpActionResult GetTimetableType()
        {
            return Ok(Enum.GetNames(typeof(TimetableType)).ToList());
        }

        [HttpGet]
        [Route("GetPricelist")]
        public IHttpActionResult GetPricelist()
        {
            PricelistViewModel model = new PricelistViewModel();
            model.TicketPrice = new Dictionary<string, int>();
            foreach (var item in UnitOfWork.TicketPrice.GetAll())
            {
                model.TicketPrice.Add(item.Type.ToString(), (int)item.Price);
            }
            return Ok(model);
        }
    }
}
