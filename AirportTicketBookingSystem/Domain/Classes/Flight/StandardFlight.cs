using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    internal class StandardFlight : Flight
    {
        public StandardFlight(string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport) : base(departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport)
        {
        }

        internal override void AddPassengerToFlight(SeatClasses seat)
        {
            if (IsThereAvailableSeat(seat))
            {
                seats[seat].AvailableSeats--;
            }
        }

        internal override bool IsThereAvailableSeat(SeatClasses seat)
        {
            return seats[seat].AvailableSeats > 0;
        }

        internal override void RemovePassengerFromFlight(SeatClasses seat)
        {
            if (seats[seat].AvailableSeats < seats[seat].MaxSeats)
            {
                seats[seat].AvailableSeats++;
            }
        }
    }
}
