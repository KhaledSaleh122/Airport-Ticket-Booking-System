using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Services
{
    internal static class PassengerService
    {
        private readonly static Dictionary<int, Passenger> passengers = new();
        internal static Dictionary<int, Passenger> GetPassengers()
        {
            return passengers.ToDictionary();
        }
        internal static void AddPassenger(Passenger passenger)
        {
            passengers.Add(passenger.Id, passenger);
        }
        internal static Passenger? GetPassenger(int id)
        {
            return passengers.FirstOrDefault(passenger => passenger.Key == id).Value;
        }
    }
}
