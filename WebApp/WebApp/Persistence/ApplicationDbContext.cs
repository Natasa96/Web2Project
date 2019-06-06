using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;
using WebApp.Models.Users;
using WebApp.Models.TrafficSystem;
using WebApp.Models.TicketService;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Passenger> PassengerDb { get; set; }
        public DbSet<NetworkLine> NetworkLineDb { get; set; }
        public DbSet<Timetable> TimetableDb { get; set; }
        public DbSet<Station> StationDb { get; set; }
        public DbSet<Ticket> TicketDb { get; set; }
        public DbSet<Pricelist> PricelistDb { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}