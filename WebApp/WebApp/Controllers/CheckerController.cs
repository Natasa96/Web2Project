using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        #region Constructor+Props
        private IUnitOfWork UnitOfWork;
        private readonly DbContext Context;
        private ApplicationUserManager _passanger;
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        public ApplicationUserManager Passanger
        {
            get
            {
                return _passanger ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _passanger = value;
            }
        }
        public CheckerController(IUnitOfWork unitOfWork, DbContext context)
        {
            Context = context;
            UnitOfWork = unitOfWork;
        }
        #endregion Constructor+Props

        [Route("GetPassengers")]
        [HttpGet]
        public IHttpActionResult GetPassengrs()
        {
            try
            {
                return Ok(ConvertToUserinfoModel(UnitOfWork.Passengers.GetAll()));
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("CheckDocument")]
        [HttpPost]
        public IHttpActionResult CheckDoucment(CheckDocumentViewModel model)
        {
            try
            {
                if(model.option == "Accepted")
                {
                    Passenger P = UnitOfWork.Passengers.Find(x => x.Id == model.id).First();
                    if (P == null)
                        return InternalServerError();
                    P.Validation = true;
                    UnitOfWork.Passengers.Update(P);
                    UnitOfWork.Complete();
                    return Ok("Document approved.");            
                }
                else
                {
                    Passenger P = UnitOfWork.Passengers.Find(x => x.Id == model.id).First();
                    if (P == null)
                        return InternalServerError();
                    if (File.Exists(P.Document))
                        File.Delete(P.Document);
                    P.Document = null;
                    P.Type = Models.Enums.PassengerType.Regular;
                    P.Validation = false;
                    UnitOfWork.Passengers.Update(P);
                    UnitOfWork.Complete();
                    return Ok("Document denied.");        
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CheckTicket")]
        [HttpPost]
        public IHttpActionResult CheckTicket(CheckTicketViewModel model)
        {
            try
            {
                Ticket ticket = UnitOfWork.Tickets.GetAll().Where(x => x.Id == model.TicketID).First();
                TicketInfoViewModel ticketModel = new TicketInfoViewModel();
                string message = string.Empty;
                switch (ticket.Type)
                {
                    case Models.Enums.TicketType.Vremenska:
                        {
                            if(ticket.ValidationTime == null)
                            {
                                ticket.ValidationTime = DateTime.Now;
                                message = "Ticket checked";
                                UnitOfWork.Complete();
                            }
                            else
                            {
                                if (ticket.ValidationTime.Value.Date.AddDays(1) > DateTime.Now.Date)
                                {
                                    message = "Ticket is valid";
                                }
                                else
                                    message = "Ticket expired";
                            }
                            break;
                        }
                    case Models.Enums.TicketType.Dnevna:
                        {
                            if (ticket.ValidationTime.Value.Date.Day < DateTime.Now.Date.Day && ticket.ValidationTime.Value.Month <= DateTime.Now.Month && ticket.ValidationTime.Value.Year <= DateTime.Now.Year)
                            {
                                message = "Ticket is valid";
                            }
                            else
                                message = "Ticket expired";
                            break;
                        }
                    case Models.Enums.TicketType.Mesecna:
                        {
                            if(DateTime.Now.Date < ticket.ValidationTime.Value.Date.AddMonths(1))
                            {
                                message = "Ticket is valid";
                            }
                            else
                            {
                                message = "Ticket expired";
                            }
                            break;
                        }
                    case Models.Enums.TicketType.Godisnja:
                        {
                            if(DateTime.Now.Date < ticket.ValidationTime.Value.Date.AddYears(1))
                            {
                                message = "Ticket is valid";
                            }
                            else
                            {
                                message = "Ticket expired";
                            }
                            break;
                        }
                    default:
                        {
                            return BadRequest("Unknown error occured");
                        }
                }
                ticketModel.Id = ticket.Id;
                ticketModel.Type = ticket.Type.ToString();
                ticketModel.Message = message;
                return Ok(ticketModel);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        private List<ValidateUserInfoViewModel> ConvertToUserinfoModel(IEnumerable<Passenger> enumerable)
        {
            List<ValidateUserInfoViewModel> model = new List<ValidateUserInfoViewModel>();
            foreach (var node in enumerable)
            {
                model.Add(new ValidateUserInfoViewModel()
                {
                    Id = node.Id,
                    Firstname = node.Firstname,
                    Lastname = node.Lastname,
                    Type = node.Type.ToString(),
                    Validation = node.Validation ? "Valid" : "Invalid",
                    Document = (node.Document!="Error" || node.Document == null) ? "http://localhost:52295/Content/" + Path.GetFileName(node.Document) : null
                });
            }
            return model;
        }

    }
}
