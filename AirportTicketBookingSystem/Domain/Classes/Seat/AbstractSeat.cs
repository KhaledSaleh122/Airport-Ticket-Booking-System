using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Seat
{
    public abstract class AbstractSeat
    {
        protected AbstractSeat(decimal seatPrice, int maxSeats)
        {
            AvailableSeats = maxSeats;
            SeatPrice = seatPrice;
            MaxSeats = maxSeats;
        }
        public abstract int AvailableSeats { get; set; }
        public abstract decimal SeatPrice { get; set; }
        public abstract int MaxSeats { get; set; }


    }
}
