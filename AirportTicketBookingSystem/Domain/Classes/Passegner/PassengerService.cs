using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Passegner
{
    public static class PassengerService
    {
        private readonly static Dictionary<int, Passenger> passengers = [];

        public static void AddPassenger(Passenger passenger)
        {
            passengers.Add(passenger.Id, passenger);
        }
        public static List<Passenger> GetPassengers()
        {
            return [.. passengers.Values];
        }
        public static Passenger? GetPassenger(int id)
        {
            return passengers.FirstOrDefault(passenger => passenger.Key == id).Value;
        }
    }
}
