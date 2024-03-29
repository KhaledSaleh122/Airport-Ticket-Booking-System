﻿using AirportTicketBookingSystem.Domain.Enums;
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
                    if (!MatchCriteriaClass(flight, criteria, criterias)) { return false; }
                }
                else if (criteria.Key == "Price")
                {
                    if (!MatchCriteriaPrice(flight, criteria, criterias)) { return false; }

                }
                else {
                    if (!MatchCriteriaProperty(flight, criteria, criterias)) return false;
                }
            }
            return true;
        }
        internal static bool Match(Flight flight, KeyValuePair<String,object> criteria,Dictionary<String,object> criterias) {
            if (criteria.Value is null) return true;
            if (criteria.Key == "Class")
            {
                if (!MatchCriteriaClass(flight,criteria,criterias)) { return false; }
            }
            else if (criteria.Key == "Price")
            {
                if (!MatchCriteriaPrice(flight, criteria, criterias)) { return false; }
            }
            else
            {
                if (!MatchCriteriaProperty(flight, criteria, criterias)) return false;
            }
            return true;
        }
        private static bool MatchCriteriaProperty(Flight flight, KeyValuePair<String, object> criteria, Dictionary<String, object> criterias) {
            var property = flight.GetType().GetProperty(criteria.Key);
            if (property is null) return true;
            if (property.PropertyType != criteria.Value?.GetType()) return true;
            if (property.PropertyType == typeof(String))
            {
                if (!String.Equals(criteria.Value as String, property.GetValue(flight) as String, StringComparison.OrdinalIgnoreCase)) return false;
            }
            else if (!Object.Equals(criteria.Value, property.GetValue(flight))) return false;
            return true;
        }
        private static bool MatchCriteriaClass(Flight flight, KeyValuePair<String, object> criteria, Dictionary<String, object> criterias) {
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
            return true;
        }
        private static bool MatchCriteriaPrice(Flight flight, KeyValuePair<String, object> criteria, Dictionary<String, object> criterias) {
            var prices = flight.ClassData.Values;
            if (criteria.Value.GetType() == typeof(int) && prices.All(classData => classData.SeatPrice > (int)criteria.Value)) return false;
            return true;
        }
        internal static void AddFlight(Flight flight)
        {
            flights.Add(flight.Id, flight);
        }
    }
}
