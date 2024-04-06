using AirportTicketBookingSystem.Domain.Classes;
using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    public abstract class AbstractFlight
    {
        private static int id = 1;
        public int Id { get; }
        public string DepartureCountry { get; }
        public string DestinationCountry { get; }
        public DateTime DepartureDate { get; }
        public string DepartureAirport { get; }
        public string ArrivalAirport { get; }

        public Dictionary<SeatClasses, AbstractSeat> seats = new(){
            { SeatClasses.Economy, new EconomySeat() },
            { SeatClasses.Business, new BusinessSeat() },
            { SeatClasses.FirstClass, new FirstClassSeat() }
        };

        public Currency Currency { get; set; } = Currency.USD;


        protected AbstractFlight(
                    string departureCountry,
                    string destinationCountry,
                    DateTime departureDate,
                    string departureAirport,
                    string arrivalAirport)
        {
            Id = id++;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
        }

        public abstract void AddPassengerToFlight(SeatClasses seat);
        public abstract bool IsThereAvailableSeat(SeatClasses seat);
        public abstract void RemovePassengerFromFlight(SeatClasses seat);

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"Flight Information: \n\nFlight Number: {Id}\nDeparture Country: {DepartureCountry}\nDestination Country: {DestinationCountry}\nDeparture Date: {DepartureDate}\nDeparture Airport: {DepartureAirport}\nArrival Airport: {ArrivalAirport}\n");
            sb.Append($"Seats Data: \n");
            sb.Append(seats[SeatClasses.Economy]);
            sb.Append('\n');
            sb.Append(seats[SeatClasses.Business]);
            sb.Append('\n');
            sb.Append(seats[SeatClasses.FirstClass]);
            sb.Append('\n');
            sb.Append($"Currency: {Currency}");
            return sb.ToString();
        }
    }
}
