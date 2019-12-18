using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models.TicketService;

namespace WebApp.Persistence.Repository.Interfaces
{
    public interface IPaypalRepository : IRepository<PaypalCredentials,int>
    {
    }
}
