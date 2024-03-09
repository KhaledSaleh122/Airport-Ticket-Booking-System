using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    internal class Book
    {
        private static int id = 1;

        public int Id { get; }
        public Flight BookedFlight { get; }
        public Passenger Passenger { get;}
        public Seat Class { get; set; }
        public int Price { get; set; }


        public Book(Flight flight, Passenger passenger,Seat SelectedClass) {
            Id = id++;
            BookedFlight = flight;
            Passenger = passenger;
            Class = SelectedClass;
            Price = BookedFlight.ClassData[Class].SeatPrice;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Book information:\n\n");
            sb.Append($"Id: {Id}\t||\tFlight Id: {BookedFlight.Id}\t||\tSeat Class: {Class}\n\n");
            sb.Append(BookedFlight);
            sb.Append("\n\n");
            sb.Append(Passenger);
            return sb.ToString();
        }

    }
}
