using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        INetworkLineRepository NetworkLines { get; set; }
        IPassengerRepository Passengers { get; set; }
        IStationRepository Stations { get; set; }
        ITicketRepository Tickets { get; set; }
        ITimetableRepositiry Timetables { get; set; }
        IPricelistRepository Pricelist { get; set; }

        int Complete();
    }
}
