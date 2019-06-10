using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;
using WebApp.Models.TicketService;
using WebApp.Models.Users;

namespace WebApp.Persistence.Repository
{
    public class PassengerRepository : Repository<Passenger, int>, IPassengerRepository
    {
        public PassengerRepository(DbContext context) : base(context) { }

        public bool BuyTicket(string id, TicketDataViewModel ticket)
        {
            try
            {
                Passenger P = Find(x => x.Id == id).First();
                Enum.TryParse(ticket.Type, out TicketType type);
                Ticket t = new Ticket() {
                    Type = type,
                    Price = ticket.Price,
                    ValidationTime = GetDate(ticket),
                    Discount = DiscountPrice.GetDiscount(P.Type),
                    Passenger = P
                };
                P.Tickets.Add(t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ValidateAppUser()
        {
            throw new NotImplementedException();
        }

        private DateTime? GetDate(TicketDataViewModel model)
        {
            if (Enum.TryParse(model.Type, out TicketType type))
            {
                if (type == TicketType.Vremenska)
                {
                    return null;
                }
                else
                    return DateTime.Now;
            }
            else
                return null;
        }
    }
}