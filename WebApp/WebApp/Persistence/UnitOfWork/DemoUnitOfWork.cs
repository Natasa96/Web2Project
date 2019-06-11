using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;
using WebApp.Persistence.Repository.Interfaces;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        [Dependency]
        public IScheduleRepository Schedules { get; set; }
        [Dependency]
        public IDepartureRepository Departures { get; set; }

        [Dependency]
        public IPricelistRepository Pricelist { get; set; }

        [Dependency]
        public INetworkLineRepository NetworkLines { get; set; }

        [Dependency]
        public IPassengerRepository Passengers { get; set; }

        [Dependency]
        public IStationRepository Stations { get; set; }

        [Dependency]
        public ITicketRepository Tickets { get; set; }

        [Dependency]
        public ITimetableRepositiry Timetables { get; set; }

        [Dependency]
        public ITicketPriceRepository TicketPrice { get; set; }

        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}