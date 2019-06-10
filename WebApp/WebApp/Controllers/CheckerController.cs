using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.TicketService;
using WebApp.Models.Users;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Controller")]
    [RoutePrefix("api/Controller")]
    public class CheckerController : ApiController
    {
        private DemoUnitOfWork _unitOfWork;

        public CheckerController() { }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

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

        [Route("CheckDocument")]
        [HttpGet]
        public IHttpActionResult GetPassengrs()
        {
            try
            {
                return Ok(UnitOfWork.Passengers.GetAll());
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("CheckDocument/id")]
        [HttpPost]
        public IHttpActionResult CheckDoucment(CheckDocumentViewModel model)
        {
            try
            {
                if(model.option == "Accepted")
                {
                    UnitOfWork.Passengers.Find(x => x.Id == model.id).First().Validation = true;
                    UnitOfWork.Complete();
                    return Ok("Your document has been accepted.");            
                }
                else
                {
                    UnitOfWork.Passengers.Find(x => x.Id == model.id).First().Document = "";
                    UnitOfWork.Passengers.Find(x => x.Id == model.id).First().Type = Models.Enums.PassengerType.Regular;
                    UnitOfWork.Complete();
                    return Ok("You didn't put a valid document.");        
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CheckTicket")]
        [HttpPost]
        public IHttpActionResult CheckTicket(Ticket model)
        {
            try
            {
                if (model.Type == Models.Enums.TicketType.Dnevna)
                    if (model.ValidationTime.Value.Day == DateTime.Today.Day)
                        return Ok($"User has valid ticket.");
                    else
                        return Ok("Ticket is not valid. Please step out off bus.");
                else if (model.Type == Models.Enums.TicketType.Vremenska)
                    if (model.ValidationTime <= model.ValidationTime.Value.AddHours(24))
                        return Ok($"User has valid ticket.");
                    else
                        return Ok("Your ticket has expired.");
                else if (model.Type == Models.Enums.TicketType.Mesecna)
                    if (model.ValidationTime <= DateTime.Now.AddMonths(1))
                        return Ok($"User has valid ticket.");
                    else
                        return Ok("Ticket is not valid. Please step out off bus.");
                else if (model.Type == Models.Enums.TicketType.Godisnja)
                    if (model.ValidationTime <= DateTime.Now.AddYears(1))
                        return Ok($"User has valid ticket.");
                    else
                        return Ok("Ticket is not valid. Please step out off bus.");
                else
                    return InternalServerError();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
