using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Passegner
{
    internal static class PassengerService
    {
        private readonly static Dictionary<int, Passenger> passengers = [];
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
