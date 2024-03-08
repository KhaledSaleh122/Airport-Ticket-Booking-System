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
        public Seat Class { get; }


        public Book(Flight flight, Passenger passenger,Seat SelectedClass) {
            Id = id++;
            this.BookedFlight = flight;
            Passenger = passenger;
            Class = SelectedClass;
        }

    }
}
