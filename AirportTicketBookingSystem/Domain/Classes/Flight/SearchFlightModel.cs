using AirportTicketBookingSystem.Domain.Classes;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    public record SearchFlightModel(
        int? Id = null,
        String? DepartureCountry = null,
        String? DestinationCountry = null,
        DateTime? DepartureDate = null,
        String? DepartureAirport = null,
        String? ArrivalAirport = null,
        SeatClasses SeatClasse = default,
        decimal? Price = null
        )
    {

        public bool Match(Flight flight)
        {
            if (DepartureDate is not null && DepartureDate != flight.DepartureDate) return false;
            return Matches(flight);
        }

        public bool MatchAvaliableFlight(Flight flight)
        {
            if (!Match(flight)) return false;
            return flight.DepartureDate >= DateTime.Now;
        }
        private bool Matches(Flight flight)
        {
            if (Id is not null && Id != flight.Id) return false;
            if (!String.IsNullOrWhiteSpace(DepartureCountry) && DepartureCountry != flight.DepartureCountry) return false;
            if (!String.IsNullOrWhiteSpace(DestinationCountry) && DestinationCountry != flight.DestinationCountry) return false;
            if (!String.IsNullOrWhiteSpace(DepartureAirport) && DepartureAirport != flight.DepartureAirport) return false;
            if (!String.IsNullOrWhiteSpace(ArrivalAirport) && ArrivalAirport != flight.ArrivalAirport) return false;
            if (SeatClasse != default)
            {
                if (flight.seats[SeatClasse].AvailableSeats == 0) return false;
            }
            if (Price is not null)
            {
                if (SeatClasse != default)
                {
                    if (Price < flight.seats[SeatClasse].SeatPrice) return false;

                }
                else
                {
                    if (
                        Price < flight.seats[SeatClasses.Economy].SeatPrice &&
                        Price < flight.seats[SeatClasses.Business].SeatPrice &&
                        Price < flight.seats[SeatClasses.FirstClass].SeatPrice
                    ) return false;
                }
            }
            return true;
        }
    }
}
