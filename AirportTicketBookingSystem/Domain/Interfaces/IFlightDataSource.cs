using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Interfaces
{
    internal interface IFlightDataSource
    {
        public List<String> LoadFlightsFromSource();
    }
}
