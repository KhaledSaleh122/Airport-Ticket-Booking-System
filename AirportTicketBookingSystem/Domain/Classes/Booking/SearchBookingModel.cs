using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Booking
{
    internal record SearchBookingModel(
        int? Id = null,
        SearchFlightModel? FlightModel = null,
        int? PassengerId = null,
        SeatClasses Class = default,
        decimal? Price = null
        )
    {
        public bool Match(Book book)
        {
            return Matches(book);
        }

        private bool Matches(Book book)
        {
            if (Id is not null && Id != book.Id) return false;
            if (FlightModel is not null && !FlightModel.Match(book.BookedFlight)) return false;
            if (PassengerId is not null && PassengerId != book.Passenger.Id) return false;
            if (Price is not null && Price < book.Price) return false;
            if (Class != default && Class != book.Class) return false;
            return true;
        }

    }
}
