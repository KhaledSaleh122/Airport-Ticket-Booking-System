using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Booking
{
    public record SearchBookingModel(
        int? Id = null,
        SearchFlightModel? FlightModel = null,
        int? PassengerId = null,
        SeatClasses Class = default,
        decimal? Price = null
        )
    {
        public bool Match(Booking booking)
        {
            return Matches(booking);
        }

        private bool Matches(Booking booking)
        {
            if (Id is not null && Id != booking.Id) return false;
            if (FlightModel is not null && !FlightModel.Match(booking.BookedFlight)) return false;
            if (PassengerId is not null && PassengerId != booking.Passenger.Id) return false;
            if (Price is not null && Price < booking.Price) return false;
            if (Class != default && Class != booking.Class) return false;
            return true;
        }

    }
}
