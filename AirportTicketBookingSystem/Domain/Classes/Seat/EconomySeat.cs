using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Seat
{
    public class EconomySeat : AbstractSeat
    {
        public EconomySeat(decimal seatPrice = 150, int maxSeats = 50) : base(seatPrice, maxSeats)
        {
        }

        public override int AvailableSeats { get; set; }
        public override decimal SeatPrice { get; set; }
        public override int MaxSeats { get; set; }
        public override string ToString()
        {
            return $"Class: 'EconomySeat' | Available Seats: '{AvailableSeats}' | Price Per Seat: '{SeatPrice}'";
        }
    }
}
