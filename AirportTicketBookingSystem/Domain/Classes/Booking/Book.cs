using AirportTicketBookingSystem.Domain.Classes;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Passegner;
using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Booking
{
    internal class Book
    {
        private static int id = 1;

        public int Id { get; }
        public AbstractFlight BookedFlight { get; }
        public Passenger Passenger { get; }
        public SeatClasses Class { get; set; }
        public decimal Price { get; set; }


        public Book(AbstractFlight flight, Passenger passenger, SeatClasses SelectedClass)
        {
            Id = id++;
            BookedFlight = flight;
            Passenger = passenger;
            Class = SelectedClass;
            Price = flight.seats[SelectedClass].SeatPrice;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Book information:\n\n");
            sb.Append($"Id: {Id}\t||\tFlight Id: {BookedFlight.Id}\t||\tSeat Class: {Class}\n\n");
            sb.Append(BookedFlight);
            sb.Append("\n\n");
            sb.Append(Passenger);
            return sb.ToString();
        }

    }
}
