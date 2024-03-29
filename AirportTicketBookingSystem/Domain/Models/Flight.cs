﻿using AirportTicketBookingSystem.Domain.Enums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    internal  class FlightSeatsData(int availableSeats, decimal seatPrice, int maxSeats)
    {
        public  int AvailableSeats { get => availableSeats; set{ if(value >= 0) availableSeats = value; } }
        public  decimal SeatPrice { get => seatPrice;set{ if(value >= 0) seatPrice = value; } }
        public  int MaxSeats { get => maxSeats; set { if (value > 0) maxSeats = value; } }
    };
    internal class Flight
    {
        private static int id = 1;
        public int Id { get; }
        public String DepartureCountry { get; }
        public String DestinationCountry { get; }
        public DateTime DepartureDate { get; }
        public String DepartureAirport { get; }
        public String ArrivalAirport { get; }
        public Dictionary<Seat, FlightSeatsData> ClassData { get; }
        public Currency Currency { get; } = Currency.USD;


        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport)
        {
            Id = id++;
            DepartureCountry = departureCountry;
            DepartureAirport = departureAirport;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            ClassData = new() { { Seat.Economy, new FlightSeatsData(50, 200, 50) }, { Seat.Business, new FlightSeatsData(20, 700, 20) }, { Seat.FirstClass, new FlightSeatsData(10, 2000, 10) } };
        }
        
        internal Flight(String departureCountry, String destinationCountry, DateTime departureDate, String departureAirport, String arrivalAirport, Currency currency, Dictionary<Seat, FlightSeatsData> classData) :
            this(departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport)
        {
            Currency = currency;
            ClassData = classData;
        }
        
        internal bool AddPassengerToFlight(Seat seat)
        {
            if(!ClassData.TryGetValue(seat,out FlightSeatsData? value)) return false;
            if (IsThereAvailableSeat(seat))
            {
                value.AvailableSeats -= 1;
                return true;
            }
            return false;
        }
        internal bool IsThereAvailableSeat(Seat seat) { 
            return ClassData[seat].AvailableSeats > 0;
        }
        internal bool RemovePassengerFromFlight(Seat seat)
        {
            if (!ClassData.TryGetValue(seat, out FlightSeatsData? value)) return false;
            if (value.AvailableSeats == value.MaxSeats) { return false; }
            value.AvailableSeats += 1;
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"Flight Information: \n\nFlight Number: {Id}\nDeparture Country: {DepartureCountry}\nDestination Country: {DestinationCountry}\nDeparture Date: {DepartureDate}\nDeparture Airport: {DepartureAirport}\nArrival Airport: {ArrivalAirport}\n");
            sb.Append($"Seats Data: \n");
            foreach (var _seat in ClassData)
            {
                sb.Append($"Class: '{_seat.Key}' | Available Seats: '{_seat.Value.AvailableSeats}' | Price Per Seat: '{_seat.Value.SeatPrice}'\n");
            }
            sb.Append($"Currency: {Currency}");
            return sb.ToString();
        }
    }
}
