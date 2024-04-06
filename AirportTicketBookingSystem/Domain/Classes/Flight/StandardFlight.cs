using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    public class StandardFlight : AbstractFlight
    {
        public StandardFlight(string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport) : base(departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport)
        {
        }

        public override void AddPassengerToFlight(SeatClasses seat)
        {
            if (IsThereAvailableSeat(seat))
            {
                seats[seat].AvailableSeats--;
            }
        }

        public override bool IsThereAvailableSeat(SeatClasses seat)
        {
            return seats[seat].AvailableSeats > 0;
        }

        public override void RemovePassengerFromFlight(SeatClasses seat)
        {
            if (seats[seat].AvailableSeats < seats[seat].MaxSeats)
            {
                seats[seat].AvailableSeats++;
            }
        }
    }
}
