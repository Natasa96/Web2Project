using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.TicketService;
using WebApp.Models.Users;

namespace WebApp.Persistence.Repository
{
    public interface IPassengerRepository : IRepository<Passenger, int>
    {
        string ValidateAppUser();

        bool BuyTicket(string id, Ticket ticket);
    }
}
