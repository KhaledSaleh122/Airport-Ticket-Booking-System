using AirportTicketBookingSystem.Domain.Classes;
using AirportTicketBookingSystem.Domain.Classes.Booking;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.UI
{
    internal static class ManagerProgram
    {
        internal static void FilterBookingsMenu()
        {
            String? userInput = String.Empty;
            Dictionary<String, Object?> props = [];
            do
            {
                Console.Clear();
                Console.WriteLine("### Filter Bookings ###");
                Console.WriteLine("## Please select the criteria you want to add for the Filter ##");
                Console.WriteLine("Enter 1: Set a specific value for the [ID] to be included in the criteria.");
                Console.WriteLine("Enter 2: Set a specific value for the [Flight ID] to be included in the criteria.");
                Console.WriteLine("Enter 3: Set a specific value for the [Passenger ID] to be included in the criteria.");
                Console.WriteLine("Enter 4: Set a specific value for the [Departure Country] to be included in the criteria.");
                Console.WriteLine("Enter 5: Set a specific value for the [Destination Country] to be included in the criteria.");
                Console.WriteLine("Enter 6: Set a specific value for the [Departure Date] to be included in the criteria.");
                Console.WriteLine("Enter 7: Set a specific value for the [Departure Airport] to be included in the criteria.");
                Console.WriteLine("Enter 8: Set a specific value for the [Arrival Airport] to be included in the criteria.");
                Console.WriteLine("Enter 9: Set a specific value for the [Class] to be included in the criteria.");
                Console.WriteLine("Enter 10: Set a specific value for the [Price] to be included in the criteria.");
                Console.WriteLine("Enter 0: To search");
                Console.WriteLine();
                Console.WriteLine(props.Count > 0 ? "Selected Criteria:" : String.Empty);
                foreach (var prop in props)
                {
                    Console.WriteLine($"Name : {prop.Key} || Value: {prop.Value}");
                }
                Console.WriteLine();
                Console.WriteLine("Your choice: ");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Enter the Id: ");
                        String? _Id = Console.ReadLine();
                        bool success = int.TryParse(_Id, out int id);
                        if (!success) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["Id"] = id;
                        break;
                    case "2":
                        Console.WriteLine("Enter the Id: ");
                        String? _FlightId = Console.ReadLine();
                        bool successFlight = int.TryParse(_FlightId, out int idFlight);
                        if (!successFlight) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["FlightId"] = idFlight;
                        break;
                    case "3":
                        Console.WriteLine("Enter the Id: ");
                        String? _PassengerId = Console.ReadLine();
                        bool _SucessPassengerId = int.TryParse(_PassengerId, out int idPassenger);
                        if (!_SucessPassengerId) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["PassengerId"] = idPassenger;
                        break;
                    case "4":
                        Console.WriteLine("Enter the Departure Country: ");
                        String? _departureCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureCountry)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureCountry"] = _departureCountry;
                        break;
                    case "5":
                        Console.WriteLine("Enter the Destination Country: ");
                        String? _destinationCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_destinationCountry)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DestinationCountry"] = _destinationCountry;
                        break;
                    case "6":
                        Console.WriteLine("Enter the Departure Date: ");
                        String? _departureDate = Console.ReadLine();
                        bool _success = DateTime.TryParse(_departureDate, out DateTime departureDate);
                        if (!_success) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureDate"] = departureDate;
                        break;
                    case "7":
                        Console.WriteLine("Enter the Departure Airport: ");
                        String? _departureAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureAirport)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureAirport"] = _departureAirport;
                        break;
                    case "8":
                        Console.WriteLine("Enter the Arrival Airport: ");
                        String? _arrivalAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_arrivalAirport)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["ArrivalAirport"] = _arrivalAirport;
                        break;
                    case "9":
                        Console.WriteLine("Choose one of these Classes: ");
                        Console.WriteLine("Enter 1: Economy seats");
                        Console.WriteLine("Enter 2: Business seats");
                        Console.WriteLine("Enter 3: FirstClass seats");
                        String? selected = Console.ReadLine();
                        switch (selected)
                        {
                            case "1": props["Class"] = SeatClasses.Economy; break;
                            case "2": props["Class"] = SeatClasses.Business; break;
                            case "3": props["Class"] = SeatClasses.FirstClass; break;
                            default: Console.WriteLine("invalid input"); Console.ReadLine(); break;
                        }
                        break;
                    case "10":
                        Console.WriteLine("Enter the price: ");
                        String? _price = Console.ReadLine();
                        bool __success = int.TryParse(_price, out int price);
                        if (!__success) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["Price"] = price;
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;

                }
            } while (userInput != "0");
            var flightSearchModel = new SearchFlightModel(
                    (int?)props.GetValueOrDefault("FlightId"),
                    (String?)props.GetValueOrDefault("DepartureCountry"),
                    (String?)props.GetValueOrDefault("DestinationCountry"),
                    (DateTime?)props.GetValueOrDefault("DepartureDate"),
                    (String?)props.GetValueOrDefault("DepartureAirport"),
                    (String?)props.GetValueOrDefault("ArrivalAirport")
            );
            var searchBookingModel = new SearchBookingModel(
                (int?)props.GetValueOrDefault("Id"),
                flightSearchModel,
                (int?)props.GetValueOrDefault("PassengerId"),
                (props.GetValueOrDefault("Class") is null ? default : (SeatClasses)props.GetValueOrDefault("Class")!),
                (decimal?)props.GetValueOrDefault("Price")
            );
            var flights = BookService.FindMatchesBooks(searchBookingModel);
            if (flights.Count == 0)
            {
                Console.WriteLine("No data match your criteria.");
            }
            Console.WriteLine();
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
                Console.WriteLine();
            }
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }
        internal static void LoadFlightFromCSV()
        {
            Console.Clear();
            Console.WriteLine("### Loading Flights From CSV File ###");
            Console.WriteLine();
            IFlightDataSource flightDataSource = new CsvFlightDataSource(@"C:\Users\khale\source\repos\AirportTicketBookingSystem\AirportTicketBookingSystem\Domain\Data\flights.csv");
            var errors = flightDataSource.LoadFlightsFromSource();
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            if (errors.Count == 0)
            {
                Console.WriteLine("\n\nAll flights from csv loaded successfully!");
            }
            else
            {
                Console.WriteLine($"\n\nThere is {errors.Count} errors during loading csv, Fix them and load it again!");
            }

            Console.WriteLine("\nPress enter to go back");
            Console.ReadLine();
        }
    }
}
