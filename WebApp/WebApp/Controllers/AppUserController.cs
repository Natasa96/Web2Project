using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models.TicketService;
using WebApp.Models.TrafficSystem;
using WebApp.Models.Users;
using WebApp.Persistence.UnitOfWork;
using WebApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebApp.Controllers
{
    [RoutePrefix("api/AppUser"), Authorize(Roles ="AppUser")]
    public class AppUserController : ApiController
    {
        private IUnitOfWork UnitOfWork;
        private DbContext Context;
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
        public AppUserController(IUnitOfWork unitOfWork, DbContext context)
        {
            Context = context;
            UnitOfWork = unitOfWork;
        }

        [Route("BuyTicket"), HttpPost]
        public async Task<IHttpActionResult> BuyTicket(Ticket ticket)
        {
            try
            {
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                if (UnitOfWork.Passengers.BuyTicket(user.Id, ticket))
                {
                    UnitOfWork.Complete();
                    return Ok();
                }
                else
                    throw new Exception("Error occured. Cannot add ticket to Passanger.");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("UpdateProfile"),HttpPost]
        public IHttpActionResult UpdateProfile(Passenger userInfo)
        {
            try
            {
                UnitOfWork.Passengers.Update(userInfo);
                UnitOfWork.Complete();
                return Ok("User info has beed updated.");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("MyInfo"),HttpGet]
        public async Task<IHttpActionResult> MyInfo()
        {
            try
            {
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                Passenger p = UnitOfWork.Passengers.Find(x => x.Id == user.Id).First();
                if (p != null)
                    return Ok(p);
                else
                    throw new Exception("User is null");
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("GetNetworkLines"),HttpGet]
        public IHttpActionResult GetNetworkLines()
        {
            try
            {
                IEnumerable<NetworkLine> lines = UnitOfWork.NetworkLines.GetAll();
                if (lines != null)
                    return Ok(lines);
                else
                    throw new Exception("Lines are null");
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("AddDocumentation"),HttpPost]
        public async Task<IHttpActionResult> AddDocumentation(DocumentationModel documentModel)
        {
            try
            {
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                Passenger p = UnitOfWork.Passengers.Find(x => x.Id == user.Id).First();
                p.Document = documentModel.document;
                p.Type = documentModel.Type;
                UnitOfWork.Passengers.Update(p);
                UnitOfWork.Complete();
                return Ok("Documentation added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
