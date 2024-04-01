using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    internal static class FlightRepository
    {
        private readonly static Dictionary<int, AbstractFlight> _flights = [];
        internal static void AddFlight(AbstractFlight flight)
        {
            _flights.Add(flight.Id, flight);
        }

        internal static Dictionary<int, AbstractFlight> GetFlights()
        {
            return new Dictionary<int, AbstractFlight>(_flights);
        }

        internal static List<AbstractFlight> FindMatchesFlight(SearchFlightModel searchFlightModel)
        {
            return _flights.Select((flightKeyValue) => flightKeyValue.Value).Where((flight) => searchFlightModel.Match(flight)).ToList();
        }
        internal static List<AbstractFlight> FindAvailableFlights(SearchFlightModel searchFlightModel)
        {
            return _flights.Select((flightKeyValue) => flightKeyValue.Value).Where((flight) => searchFlightModel.MatchAvaliableFlight(flight)).ToList();
        }
    }
}
