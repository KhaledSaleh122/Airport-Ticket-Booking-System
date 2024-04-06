using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    public static class FlightRepository
    {
        private readonly static Dictionary<int, AbstractFlight> _flights = [];
        public static void AddFlight(AbstractFlight flight)
        {
            _flights.Add(flight.Id, flight);
        }

        public static Dictionary<int, AbstractFlight> GetFlights()
        {
            return new Dictionary<int, AbstractFlight>(_flights);
        }

        public static List<AbstractFlight> FindMatchesFlight(SearchFlightModel searchFlightModel)
        {
            return _flights.Select((flightKeyValue) => flightKeyValue.Value).Where((flight) => searchFlightModel.Match(flight)).ToList();
        }
        public static List<AbstractFlight> FindAvailableFlights(SearchFlightModel searchFlightModel)
        {
            return _flights.Select((flightKeyValue) => flightKeyValue.Value).Where((flight) => searchFlightModel.MatchAvaliableFlight(flight)).ToList();
        }
    }
}
