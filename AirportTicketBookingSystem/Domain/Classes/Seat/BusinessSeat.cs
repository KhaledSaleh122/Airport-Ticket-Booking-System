using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Seat
{
    public class BusinessSeat : AbstractSeat
    {
        public BusinessSeat(decimal seatPrice = 750, int maxSeats = 30) : base(seatPrice, maxSeats)
        {
        }

        public override int AvailableSeats { get; set; }
        public override decimal SeatPrice { get; set; }
        public override int MaxSeats { get; set; }

        public override string ToString()
        {
            return $"Class: 'BusinessSeat' | Available Seats: '{AvailableSeats}' | Price Per Seat: '{SeatPrice}'";
        }
    }
}
