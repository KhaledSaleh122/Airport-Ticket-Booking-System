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
    internal static class FlightService
    {
        private readonly static Dictionary<int, Flight> flights = [];

        public static List<Flight> Search(Dictionary<String,Object>? criteria,bool igonreDateVlidation = false) {
           return flights.Select((flight) => flight.Value).Where((flight) => ((!igonreDateVlidation && flight.DepartureDate >= DateTime.Now) || igonreDateVlidation) && Match(flight, criteria)).ToList();
        }
        public static List<Flight> Search(bool igonreDateVlidation = false) {
            return Search(null, igonreDateVlidation);
        }
        private static bool Match(Flight flight, Dictionary<String, Object> criterias) {
            if (criterias is null) return true;
            //if no available seat don't include it
            if (flight.ClassData.Values.All(numberOfSeats=> numberOfSeats.AvailableSeats == 0)) return false;
            //Go over all criterias and check equality with  properites in flight
            foreach (var criteria in criterias) {
                //custom criteria 'Class' & 'Price' to Show records based on seat type and price
                if (criteria.Value is null) continue;
                if (criteria.Key == "Class")
                {
                    var seat = criteria.Value;
                    bool sucess = Enum.IsDefined(typeof(Seat), seat);
                    if (sucess)
                    {
                        FlightSeatsData value = flight.ClassData[(Seat)seat];
                        if (value.AvailableSeats == 0) return false;
                        if (criterias.TryGetValue("Price", out object? priceValue))
                        {
                            if (priceValue is not null && priceValue.GetType() == typeof(int) && value.SeatPrice > (int)priceValue) return false;
                        }
                    }
                }
                else if (criteria.Key == "Price")
                {
                    var prices = flight.ClassData.Values;
                    if (criteria.Value.GetType() == typeof(int) && prices.All(classData => classData.SeatPrice > (int)criteria.Value)) return false;

                }
                else {
                    var property = flight.GetType().GetProperty(criteria.Key);
                    if (property is null) continue;
                    if (property.PropertyType != criteria.Value?.GetType()) continue;
                    if (property.PropertyType == typeof(String))
                    {
                        if (!String.Equals(criteria.Value as String, property.GetValue(flight) as String, StringComparison.OrdinalIgnoreCase)) return false;
                    }
                    else if (!Object.Equals(criteria.Value, property.GetValue(flight))) return false;
                }
            }
            return true;
        }
        internal static void AddFlight(Flight flight)
        {
            flights.Add(flight.Id, flight);
        }
    }
}
