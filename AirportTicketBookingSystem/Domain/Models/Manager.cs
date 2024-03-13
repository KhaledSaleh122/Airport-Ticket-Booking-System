using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    internal static class Manager
    {
        private class CSVFlightFields(string name, string errorMessage, bool required, Type type)
        {
            public String Name { get; set; } = name;
            public String ErrorMessage { get; set; } = errorMessage;
            public bool Required { get; set; } = required;
            public Type Type { get; set; } = type;

            public Object? currValue { get;set; }
        }
        internal static List<String> LoadFlightsDataFromCSV(){ 
            List<String> erros = [];
            List<CSVFlightFields> list =
            [
                    new("DepartureCountry", "- *DepartureCountry:*\r\n    - Type: String\r\n    - Constraint: Required",true,typeof(string)),
                    new("DestinationCountry", "- *DestinationCountry:*\r\n    - Type: String\r\n    - Constraint: Required",true,typeof(string)),
                    new("DepartureDate","- *DepartureDate:*\r\n    - Type: DateTime\r\n    - Constraint: Required, Allowed Range (today → future)",true,typeof(DateTime)),
                    new("DepartureAirport", "- *DepartureAirport:*\r\n    - Type: String\r\n    - Constraint: Required", true,typeof(string)),
                    new("ArrivalAirport", "- *ArrivalAirport:*\r\n    - Type: String\r\n    - Constraint: Required", true,typeof(string)),
                    new("Currency", "- *Currency:*\r\n    - Type: String\r\n    - Constraint: Optional,USD/EUR/ILS", false,typeof(Currency)),
                    new("MaxSeatsClassEconomy", "- *MaxSeatsClassEconomy:*\r\n    - Type: Int32\r\n    - Constraint: Optional, Positive Number", false,typeof(int)),
                    new("PricePerSeatClassEconomy", "- *PricePerSeatClassEconomy:*\r\n    - Type: Decimal\r\n    - Constraint: Optional, Non-negative Number", false,typeof(decimal)),
                    new("MaxSeatsClassBusiness", "- *MaxSeatsClassBusiness:*\r\n    - Type: Int32\r\n    - Constraint: Optional, Positive Number", false,typeof(int)),
                    new("PricePerSeatClassBusiness", "- *PricePerSeatClassBusiness:*\r\n    - Type: Decimal\r\n    - Constraint: Optional, Non-negative Number", false,typeof(decimal)),
                    new("MaxSeatsFirstClass", "- *MaxSeatsFirstClass:*\r\n    - Type: Int32\r\n    - Constraint: Optional, Positive Number", false,typeof(int)),
                    new("PricePerSeatFirstClass", "- *PricePerSeatFirstClass:*\r\n    - Type: Decimal\r\n    - Constraint: Optional, Non-negative Number", false,typeof(decimal))
             ];
            List<Flight> flights = [];
            using (var reader = new StreamReader(@"C:\Users\khale\source\repos\AirportTicketBookingSystem\AirportTicketBookingSystem\Domain\Data\flights.csv"))
            {
                int count=0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count == 0) { ++count; continue; }
                    if (line == null) continue;
                    var values = line.Split(',');
                    for (int i = 0; i < list.Count; i++) {
                        if (values.Length - 1 >= i && !Validate(values[i], list[i])) {
                            erros.Add($"Line {count+1}:\n{list[i].ErrorMessage}");
                        }
                    }
                    if (erros.Count == 0) {
                        Currency currncy = Currency.USD;
                        var classData = new Dictionary<Seat, FlightSeatsData>() { { Seat.Economy, new FlightSeatsData(50, 200, 50) }, { Seat.Business, new FlightSeatsData(20, 700, 20) }, { Seat.FirstClass, new FlightSeatsData(10, 2000, 10) } };
                        if (list[5].currValue is not null) currncy = (Currency)list[5].currValue!;
                        if (list[6].currValue is not null) { classData[Seat.Economy].AvailableSeats = (int)list[6].currValue!; classData[Seat.Economy].MaxSeats = (int)list[6].currValue!; }
                        if (list[7].currValue is not null) { classData[Seat.Economy].SeatPrice = (decimal)list[7].currValue!;}
                        if (list[8].currValue is not null) { classData[Seat.Business].AvailableSeats = (int)list[8].currValue!; classData[Seat.Business].MaxSeats = (int)list[8].currValue!; }
                        if (list[9].currValue is not null) { classData[Seat.Business].SeatPrice = (decimal)list[9].currValue!;}
                        if (list[10].currValue is not null) { classData[Seat.FirstClass].AvailableSeats = (int)list[10].currValue!; classData[Seat.FirstClass].MaxSeats = (int)list[10].currValue!; }
                        if (list[11].currValue is not null) { classData[Seat.FirstClass].SeatPrice = (decimal)list[11].currValue!;}
                        flights.Add(
                            new Flight((String)list[0].currValue!, (String)list[1].currValue!, (DateTime)list[2].currValue!, (String)list[3].currValue!, (String)list[4].currValue!, currncy, classData)
                        );
                    }
                    count++;
                }
            }
            if (erros.Count == 0) {
                foreach (Flight flight in flights)
                {
                    FlightService.AddFlight(flight);
                }
            }
            return erros;
        }

        private static bool Validate(String value, CSVFlightFields cff) {
            if (cff.Required && String.IsNullOrWhiteSpace(value)) { return false; }
            else if (!cff.Required && String.IsNullOrWhiteSpace(value)) { return true; }
            if (cff.Type == typeof(int))
            {
                bool success = int.TryParse(value, out var val);
                if (!success) { return false; }
                if (cff.Name.StartsWith("MaxSeats") && val <= 0) return false;
                cff.currValue = val;
            }
            else if (cff.Type == typeof(decimal))
            {
                bool success = decimal.TryParse(value, out decimal val);
                if (!success) return false;
                if (cff.Name.StartsWith("PricePerSeat") && val < 0)
                {
                    return false;
                }
                cff.currValue = val;
            }
            else if (cff.Type == typeof(DateTime))
            {
                bool success = DateTime.TryParse(value, out DateTime val);
                if (!success) { return false; }
                if (cff.Name == "DepartureDate" && val < DateTime.Now) return false;
                cff.currValue = val;
            }
            else if (cff.Type == typeof(Currency))
            {
                bool sucess = Enum.TryParse(value, out Currency result);
                if (!sucess) return false;
                cff.currValue = result;
            }
            else if (cff.Type == typeof(string))
            {
                cff.currValue = value;
            }
            else
            { 
                return false ;
            }
            return true;
        }
    }
}
