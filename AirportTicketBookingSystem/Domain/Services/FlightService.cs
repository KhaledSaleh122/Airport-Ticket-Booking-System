using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirportTicketBookingSystem.Domain.Services
{
    internal class FlightService
    {
        private readonly static Dictionary<int, Flight> flights = [];

        public static List<Flight> Search(Dictionary<String,Object>? criteria) {
           return flights.Select((flight) => flight.Value).Where((flight) => Match(flight, criteria)).ToList();
        }
        public static List<Flight> Search() {
            return Search(null);
        }
        private static bool Match(Flight flight, Dictionary<String, Object> criterias) {
            var flightType = flight.GetType();
            if (criterias is null) return true;
            //if no available seat don't include it
            int availableSeatsCount = flight.AvailableSeats.Select((f) => f.Value).Sum();
            if (availableSeatsCount == 0) return false;
            //custom criteria 'Class' to Show records based on seat type
            if (criterias.ContainsKey("Class"))
            {
                var availableSeatsProperty = flightType.GetProperty("AvailableSeats");
                var seat = criterias["Class"];
                bool sucess = Enum.IsDefined(typeof(Seat), seat!);
                if (sucess) { 
                    Dictionary<Seat, int> availableSeats = (Dictionary<Seat, int>)availableSeatsProperty!.GetValue(flight)!;
                    if (availableSeats[(Seat)seat!] == 0) return false;
                }
            }
            //Go over all criterias and check equality with  properites in flight
            foreach (var criteria in criterias) { 
                 var property = flight.GetType().GetProperty(criteria.Key);
                if (property is null) continue;
                if (property.PropertyType != criteria.Value.GetType()) continue;
                if (property.PropertyType == typeof(String)) {
                    if (!String.Equals(criteria.Value as String, property.GetValue(flight) as String, StringComparison.OrdinalIgnoreCase)) return false;
                }
                else if (!Object.Equals(criteria.Value, property.GetValue(flight))) return false;
            }
            return true;
        }
        internal static void AddFlight(Flight flight)
        {
            flights.Add(flight.Id, flight);
        }
    }
}
