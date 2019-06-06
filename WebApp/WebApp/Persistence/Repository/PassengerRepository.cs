using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.TicketService;
using WebApp.Models.Users;

namespace WebApp.Persistence.Repository
{
    public class PassengerRepository : Repository<Passenger, int>, IPassengerRepository
    {
        public PassengerRepository(DbContext context) : base(context) { }

        public bool BuyTicket(string id, Ticket ticket)
        {
            try
            {
                Passenger P = Find(x => x.Id == id).First();
                P.Tickets.Add(ticket);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public string ValidateAppUser()
        {
            throw new NotImplementedException();
        }
    }
}