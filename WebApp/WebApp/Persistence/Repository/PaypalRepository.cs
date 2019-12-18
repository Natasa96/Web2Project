using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models.TicketService;
using WebApp.Persistence.Repository.Interfaces;

namespace WebApp.Persistence.Repository
{
    public class PaypalRepository : Repository<PaypalCredentials, int>, IPaypalRepository
    {
        public PaypalRepository(DbContext context) : base(context)
        {
        }
    }
}