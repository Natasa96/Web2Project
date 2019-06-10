using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;
using WebApp.Persistence.Repository.Interfaces;

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
        ITicketPriceRepository TicketPrice { get; set; }

        int Complete();
    }
}
