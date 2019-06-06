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
        public IHttpActionResult AddDocumentation()
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                try
                {

                    var httpRequest = HttpContext.Current.Request;

                    foreach (string file in httpRequest.Files)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

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

                                dict.Add("error", message);
                                return Ok();
                            }
                            else if (postedFile.ContentLength > MaxContentLength)
                            {

                                var message = string.Format("Please Upload a file upto 1 mb.");

                                dict.Add("error", message);
                                return Ok();
                            }
                            else
                            {



                                var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                                postedFile.SaveAs(filePath);

                            }
                        }

                        var message1 = string.Format("Image Updated Successfully.");
                        return Ok();
                    }
                    var res = string.Format("Please Upload a image.");
                    dict.Add("error", res);
                    return Ok();
                }
                catch (Exception ex)
                {
                    var res = string.Format("some Message");
                    dict.Add("error", res);
                    return Ok();
                }


                //if(!ModelState.IsValid)
                //{
                //    return BadRequest();
                //}
                //IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());

                //if (user == null)
                //{
                //    return null;
                //}

                //Passenger p = UnitOfWork.Passengers.Find(x => x.Id == user.Id).First();
                //p.Document = documentModel.document;
                //p.Type = documentModel.Type;
                //UnitOfWork.Passengers.Update(p);
                //UnitOfWork.Complete();

                return Ok("Documentation added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
