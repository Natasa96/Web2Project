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
        public AppUserController(IUnitOfWork unitOfWork, DbContext context)
        {
            Context = context;
            UnitOfWork = unitOfWork;
        }
        #endregion Constructor+Props

        [Route("BuyTicket"), HttpPost]
        public async Task<IHttpActionResult> BuyTicket(TicketDataViewModel model)
        {
            try
            {
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                if (UnitOfWork.Passengers.BuyTicket(user.Id, model))
                {
                    UnitOfWork.Complete();
                    UnitOfWork.Paypal.Add(new PaypalCredentials()
                    {
                        Address = model.Address,
                        Id = model.Id,
                        CreateTime = model.CreateTime,
                        FullName = model.FullName,
                        PurchaseUnit = model.PurchaseUnit,
                        Status = model.Status,
                        Ticket = UnitOfWork.Tickets.GetAll().LastOrDefault(),
                        UpdateTime = model.UpdateTime
                    });
                    UnitOfWork.Complete();
                    return Ok("Ticket purchased!");
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
        public async Task<IHttpActionResult> UpdateProfile()
        {
            try
            {
                var path = SaveImage();
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());
                if (user == null)
                    return BadRequest();
                Passenger P = UnitOfWork.Passengers.GetAll().Where(x => x.Id == user.Id).First();
                P.Document = path;
                P.UserName = HttpContext.Current.Request["Username"];
                P.Firstname = HttpContext.Current.Request["Firstname"];
                P.Lastname = HttpContext.Current.Request["Lastname"];
                P.Email = HttpContext.Current.Request["Email"];
                P.Address = HttpContext.Current.Request["Address"];
                P.Type =(PassengerType)Enum.Parse(typeof(PassengerType),HttpContext.Current.Request["Type"]);
                UnitOfWork.Passengers.Update(P);
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
                string path = SaveImage();
                if (path != "Error")
                {
                    IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());
                    if (user == null)
                        return InternalServerError();
                    var typeP = HttpContext.Current.Request.Params["UserType"];
                    Enum.TryParse(typeP, out PassengerType type);
                    Passenger p = UnitOfWork.Passengers.Find(x => x.Id == user.Id).First();
                    p.Document = path;
                    p.Type = type;
                    UnitOfWork.Passengers.Update(p);
                    UnitOfWork.Complete();
                    var message1 = string.Format("Documentation added.");
                    return Ok(message1);
                }
                else
                    return BadRequest("Cannot add image");

             }
             catch (Exception ex)
             {
                 return Ok(ex.Message);
             }
        }
        [Route("GetPricelist"),HttpGet]
        public IHttpActionResult GetPricelist()
        {
            List<string> TicketTypes = new List<string>();
            var pricelist = UnitOfWork.Pricelist.GetAll().Where(x=> x.Active == true).First();
            foreach(var node in UnitOfWork.TicketPrice.GetAll().Where(x=> x.Pricelist.Id == pricelist.Id))
            {
                TicketTypes.Add(node.Type.ToString());
            }
            return Ok(TicketTypes);
        }
        [Route("CalculatePrice"),HttpPost]
        public async Task<IHttpActionResult> CalculatePrice(GetPriceViewModel model)
        {
            try
            {
                var pricelist = UnitOfWork.Pricelist.GetAll().Where(x => x.Active == true).First();

                int price = (int)UnitOfWork.TicketPrice.GetAll().Where(x => x.Pricelist.Id == pricelist.Id).Where(y=> y.Type.ToString() == model.Type).First().Price;
                IdentityUser user = await Passanger.FindByIdAsync(User.Identity.GetUserId());
                if (user == null)
                    return InternalServerError();
                Passenger p = UnitOfWork.Passengers.GetAll().Where(x => x.Id == user.Id).First();
                if (p.Type == PassengerType.Regular || (p.Type != PassengerType.Regular && p.Validation == false))
                {
                    return Ok(price);
                }
                else
                {
                    price = price - price * (DiscountPrice.GetDiscount(p.Type) / 100);
                    return Ok(price);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #region Utils
        /// <summary>
        /// Converts Passenger class into PassengerInfoViewModel used in client
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
                Document = (p.Document != null) ?"http://localhost:52295/Content/"+ Path.GetFileName(p.Document) : null,
                Validation = p.Validation,
                Type = p.Type.ToString(),
                Types = Enum.GetNames(typeof(PassengerType)).ToList()
            };
        }
        
        /// <summary>
        /// Saves image to Content folder.
        /// Returns path to the file.
        /// </summary>
        /// <returns>Path to the file.</returns>
        private string SaveImage()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
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
                        return "Error";
                    }
                    else if (postedFile.ContentLength > MaxContentLength)
                    {
                        return "Error";
                    }
                    else
                    {
                        //var Filename = "Document" + UnitOfWork.Passengers.GetAll().Count().ToString() + extension;
                        var filePath = string.Format(System.Web.Hosting.HostingEnvironment.MapPath("~/Content") + "\\" + postedFile.FileName);
                        if(UnitOfWork.Passengers.GetAll().ToList().Exists(x=> x.Document == filePath))
                        {
                            postedFile.SaveAs(filePath);
                            return filePath;
                        }
                        else
                        {
                            string[] parts = postedFile.FileName.Split('.');
                            int copynum = 1;
                            while (true)
                            {
                                var newfile = string.Format(System.Web.Hosting.HostingEnvironment.MapPath("~/Content") + "\\" + parts[0]+ copynum.ToString()+"."+parts[1]);
                                if (!UnitOfWork.Passengers.GetAll().ToList().Exists(x=> x.Document == newfile))
                                {
                                    postedFile.SaveAs(newfile);
                                    return newfile;
                                }
                                copynum++;
                            }

                        }
                        
                        
                    }
                }
            }
            return "Error";
        }
        #endregion Utils
    }
}
