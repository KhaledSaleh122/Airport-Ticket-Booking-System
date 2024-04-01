using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes
{
    internal static class FlightValidators
    {
        public static readonly List<FieldValidatorModel> Validators =
        [
            new FieldValidatorModel(
                typeof(string),
                "DepartureCountry",
                true,
                (departureCountry) => !String.IsNullOrWhiteSpace((string)departureCountry)
            ),
            new FieldValidatorModel(
                typeof(string),
                "DestinationCountry",
                true,
                (destinationCountry) => !String.IsNullOrWhiteSpace((string)destinationCountry)
            ),
            new FieldValidatorModel(
               typeof(DateTime),
               "DepartureDate",
               true,
               (departureDate) => (DateTime)departureDate >= DateTime.Now,
               "Allowed Range (today → future)"
            ),
            new FieldValidatorModel(
                typeof(string),
                "DepartureAirport",
                true,
                (departureAirport) => !String.IsNullOrWhiteSpace((string)departureAirport)
            ),
            new FieldValidatorModel(
                typeof(string),
                "ArrivalAirport",
                true,
                (arrivalAirport) => !String.IsNullOrWhiteSpace((string)arrivalAirport)
            ),
            new FieldValidatorModel(
               typeof(Currency),
               "Currency",
               false,
               (currency) => true,
               "Allowed values USD/EUR/ILS"
            ),
            new FieldValidatorModel(
                typeof(int),
                "MaxSeatsClassEconomy",
                false,
                (maxSeats) => (int)maxSeats > 0,
                "Value must be Positive Number"
            ),
            new FieldValidatorModel(
                typeof(decimal),
                "PricePerSeatClassEconomy",
                false,
                (pricePerSeat) => (decimal)pricePerSeat >= 0,
                "Value must beNon-negative Number"
            ),
            new FieldValidatorModel(
                typeof(int),
                "MaxSeatsClassBusiness",
                false,
                (maxSeats) => (int)maxSeats > 0,
                "Value must be Positive Number"
            ),
            new FieldValidatorModel(
                typeof(decimal),
                "PricePerSeatClassBusiness",
                false,
                (pricePerSeat) => (decimal)pricePerSeat >= 0,
                "Value must beNon-negative Number"
            ),
            new FieldValidatorModel(
                typeof(int),
                "MaxSeatsFirstClass",
                false,
                (maxSeats) => (int)maxSeats > 0,
                "Value must be Positive Number"
            ),
            new FieldValidatorModel(
                typeof(decimal),
                "PricePerSeatFirstClass",
                false,
                (pricePerSeat) => (decimal)pricePerSeat >= 0,
                "Value must beNon-negative Number"
            )
        ];
    }
}
