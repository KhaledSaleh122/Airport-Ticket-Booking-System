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
        private static int Id = 1;
        private int id;
        private String departureCountry = String.Empty;
        private String destinationCountry = String.Empty;
        private DateTime departureDate;
        private String departureAirport = String.Empty;
        private String arrivalAirport = String.Empty;
        private Dictionary<Seat, int> availableSeats;
        private Dictionary<Seat, int> flightPrice;
        private Currency currency;
        private Dictionary<Seat, int> maxSeats = new();

        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport) { 
            id = Id++;
            this.departureCountry= departureCountry;
            this.departureAirport=departureAirport;
            this.destinationCountry=destinationCountry;
            this.departureDate=departureDate;
            this.departureAirport = departureAirport;
            this.arrivalAirport=arrivalAirport;
            availableSeats = new() { { Seat.Economy, 50}, { Seat.Business, 10}, { Seat.FirstClass, 5} };
            foreach (var _availableSeats in availableSeats) {
                maxSeats.Add(_availableSeats.Key, _availableSeats.Value);
            }
            flightPrice = new() { { Seat.Economy, 100 }, { Seat.Business, 700 }, { Seat.FirstClass, 900 } };
            currency = Currency.USD;
        }
        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport, Dictionary<Seat, int> availableSeats, Dictionary<Seat, int> flightPrice, Currency currency)
        {
            id = Id++;
            this.departureCountry = departureCountry;
            this.departureAirport = departureAirport;
            this.destinationCountry = destinationCountry;
            this.departureDate = departureDate;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
            this.availableSeats = availableSeats;
            foreach (var _availableSeats in availableSeats)
            {
                maxSeats.Add(_availableSeats.Key, _availableSeats.Value);
            }
            this.flightPrice = flightPrice;
            this.currency = currency;
        }
        internal bool AddPassengerToFlight(Seat seat) {
            if (!availableSeats.TryGetValue(seat, out int availableSeatsLeft)) {
                return false;
            }
            if (availableSeatsLeft > 0) {
                availableSeats[seat] -= 1;
                return true;
            }
            return false;
        }        
        internal bool removePassengerFromFlight(Seat seat) {
            if (!availableSeats.TryGetValue(seat, out int availableSeatsLeft))
            {
                return false;
            }
            if (availableSeatsLeft == maxSeats[seat]) { return false; }
            availableSeats[seat] += 1;
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Flight Information: \nFlight Number: {id}\nDepartureCountry: {departureCountry}\nDestination Country: {destinationCountry}\nDeparture Date: {departureDate}\nDeparture Airport: {departureAirport}\nArrival Airport: {arrivalAirport}\n");
            sb.Append($"Available Seats: ");
            foreach (var _seat in availableSeats)
            {
                sb.Append($" {_seat.Key} Seat: {_seat.Value}");
            }
            sb.Append($"\nPrice Per Seat: ");
            foreach (var _price in flightPrice) {
                sb.Append($" {_price.Key} Seat: {_price.Value} ");
            }
            sb.Append($"\nCurrency: {currency}");
            return sb.ToString();
        }
    }
}
