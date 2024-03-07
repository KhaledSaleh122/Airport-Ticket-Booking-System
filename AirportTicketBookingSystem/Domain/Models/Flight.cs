using AirportTicketBookingSystem.Domain.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    internal class Flight
    {
        private static int id = 1;
        public int Id { get; }
        public String DepartureCountry { get; }
        public String DestinationCountry { get; }
        public DateTime DepartureDate { get; }
        public String DepartureAirport { get; }
        public String ArrivalAirport { get; }
        public Dictionary<Seat, int> AvailableSeats { get; }
        public Dictionary<Seat, int> FlightPrice { get; }
        public Currency Currency { get; }
        public Dictionary<Seat, int> MaxSeats { get; } = [];
        

        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport) { 
            Id = id++;
            DepartureCountry = departureCountry;
            DepartureAirport = departureAirport;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            AvailableSeats = new() { { Seat.Economy, 50}, { Seat.Business, 10}, { Seat.FirstClass, 5} };
            foreach (var _availableSeats in AvailableSeats) {
                MaxSeats.Add(_availableSeats.Key, _availableSeats.Value);
            }
            FlightPrice = new() { { Seat.Economy, 100 }, { Seat.Business, 700 }, { Seat.FirstClass, 900 } };
            Currency = Currency.USD;
        }
        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport, Dictionary<Seat, int> availableSeats, Dictionary<Seat, int> flightPrice, Currency currency)
        {
            Id = id++;
            DepartureCountry = departureCountry;
            DepartureAirport = departureAirport;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            AvailableSeats = availableSeats;
            foreach (var _availableSeats in availableSeats)
            {
                MaxSeats.Add(_availableSeats.Key, _availableSeats.Value);
            }
            FlightPrice = flightPrice;
            Currency = currency;
        }
        internal bool AddPassengerToFlight(Seat seat) {
            if (!AvailableSeats.TryGetValue(seat, out int availableSeatsLeft)) {
                return false;
            }
            if (availableSeatsLeft > 0) {
                AvailableSeats[seat] -= 1;
                return true;
            }
            return false;
        }        
        internal bool RemovePassengerFromFlight(Seat seat) {
            if (!AvailableSeats.TryGetValue(seat, out int availableSeatsLeft))
            {
                return false;
            }
            if (availableSeatsLeft == MaxSeats[seat]) { return false; }
            AvailableSeats[seat] += 1;
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"Flight Information: \nFlight Number: {Id}\nDeparture Country: {DepartureCountry}\nDestination Country: {DestinationCountry}\nDeparture Date: {DepartureDate}\nDeparture Airport: {DepartureAirport}\nArrival Airport: {ArrivalAirport}\n");
            sb.Append($"Available Seats: ");
            foreach (var _seat in AvailableSeats)
            {
                sb.Append($" {_seat.Key} Seat: {_seat.Value}");
            }
            sb.Append($"\nPrice Per Seat: ");
            foreach (var _price in FlightPrice) {
                sb.Append($" {_price.Key} Seat: {_price.Value} ");
            }
            sb.Append($"\nCurrency: {Currency}");
            return sb.ToString();
        }
    }
}
