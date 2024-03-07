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
    static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type type)
        {
            return type.IsClass
                   && type.IsSealed
                   && type.BaseType == typeof(object)
                   && type.Name.StartsWith("<>f__AnonymousType", StringComparison.Ordinal)
                   && type.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false).Length > 0;
        }
    }
    internal class FlightService
    {
        private readonly static Dictionary<int, Flight> flights = [];

        public static List<Flight> Search(Object? criteria) {
           return flights.Select((flight) => flight.Value).Where((flight) => Match(flight, criteria)).ToList();
        }
        public static List<Flight> Search() {
            return Search(null);
        }
        private static bool Match(Flight flight, Object criteria) {
            var criteriaType = criteria.GetType();
            var flightType = flight.GetType();
            //make sure that criteria class is anonymouse
            if (criteria is null || !criteriaType.IsAnonymousType()) return true;
            //if no available seat don't include it
            int availableSeatsCount = flight.AvailableSeats.Select((f) => f.Value).Sum();
            if (availableSeatsCount == 0) return false;
            //custom criteria 'Class' to Show records based on seat type
            if (criteriaType?.GetProperty("Class")?.GetValue(criteria) is not null)
            {
                var availableSeatsProperty = flightType.GetProperty("AvailableSeats");
                var seat = criteriaType.GetProperty("Class")!.GetValue(criteria);
                bool sucess = Enum.IsDefined(typeof(Seat), seat!);
                if (sucess) { 
                    Dictionary<Seat, int> availableSeats = (Dictionary<Seat, int>)availableSeatsProperty!.GetValue(flight)!;
                    if (availableSeats[(Seat)seat!] == 0) return false;
                }
            }
            //Go over all properites in flight class and check equality 'only' with provided properites in anonymouse class
            foreach (var property in flightType.GetProperties()) {
                var criteriaProperty = criteriaType!.GetProperty(property.Name);
                if (criteriaProperty is null) continue;
                //make sure both criteria Property and flight Property have same type
                if (property.PropertyType != criteriaProperty.PropertyType) continue;
                if (property.PropertyType == typeof(String))
                {   //check equlaity between strings and ignore case sensitive
                    if (!String.Equals(criteriaProperty.GetValue(property.Name) as String, property.GetValue(property.Name) as String, StringComparison.OrdinalIgnoreCase)) return false;
                }
                else if (!Object.Equals(criteriaProperty.GetValue(criteria), property.GetValue(flight))) return false;
            }
            return true;
        }
        internal static void AddFlight(Flight flight)
        {
            flights.Add(flight.Id, flight);
        }
    }
}
