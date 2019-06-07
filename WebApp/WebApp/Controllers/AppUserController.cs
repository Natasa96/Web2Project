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
using System.IO;
using System.Drawing;
using System.Web;
using WebApp.Models.Enums;
using System.Globalization;

namespace WebApp.Controllers
{
    [RoutePrefix("api/AppUser"), Authorize(Roles = "AppUser")]
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("UpdateProfile"), HttpPost]
        public IHttpActionResult UpdateProfile(Passenger userInfo)
        {
            try
            {
                UnitOfWork.Passengers.Update(userInfo);
                UnitOfWork.Complete();
                return Ok("User info has beed updated.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("MyInfo"), HttpGet]
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
                    return Ok(CreatePassengerInfo(p));
                else
                    throw new Exception("User is null");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("GetNetworkLines"), HttpGet]
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
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        [Route("AddDocumentation"), HttpPost]
        public async Task<IHttpActionResult> AddDocumentation()
        {
             try
             {
                var httpRequest = HttpContext.Current.Request;
                var typeP = HttpContext.Current.Request.Params["UserType"];
                string path = string.Empty;
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  
                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                            return Ok(message);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");
                            return Ok(message);
                        }
                        else
                        {
                            var filePath = @"C:\Users\Nenad\Desktop\Web2Project\WebApp\WebApp\Content\" + postedFile.FileName + extension;
                            path = filePath;
                            postedFile.SaveAs(filePath);
                        }
                    }
                    IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());
                    if (user == null)
                        return InternalServerError();

                    Passenger p = UnitOfWork.Passengers.Find(x => x.Id == user.Id).First();
                    p.Document = path;
                    p.Type = ConvertType(typeP);
                    UnitOfWork.Passengers.Update(p);
                    UnitOfWork.Complete();
                    var message1 = string.Format("Documentation added.");
                    return Ok(message1);
                }
                var res = string.Format("Please Upload a image.");
                return Ok(res);
             }
             catch (Exception ex)
             {
                 return Ok(ex.Message);
             }
        }
        private PassengerInfoViewModel CreatePassengerInfo(Passenger p)
        {
            return new PassengerInfoViewModel()
            {
                Username = p.UserName,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Address = p.Address,
                Email = p.Email,
                Birthdate = p.Birthdate.Value.Date,
                Document = p.Document,
                Validation = p.Validation
            };
        }
        private PassengerType ConvertType(string typeP)
        {
            switch(typeP)
            {
                case "Student": return PassengerType.Student;
                case "Penzioner": return PassengerType.Penzioner;
                default: return PassengerType.Regular;
            }
        }
    }
}
