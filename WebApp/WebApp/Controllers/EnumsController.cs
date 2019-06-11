using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models.Enums;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Enums")]
    public class EnumsController : ApiController
    {
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
        [Route("GetTimetableType")]
        public IHttpActionResult GetTimetableType()
        {
            return Ok(Enum.GetNames(typeof(TimetableType)).ToList());
        }
    }
}
