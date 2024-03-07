﻿using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using AirportTicketBookingSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AirportTicketBookingSystem.Domain.UI
{
    internal class PassengerProgram
    {
        internal static void AddPassenger()
        {
            String userInput = String.Empty;
            String name = String.Empty;
            Console.Clear();
            do
            {
                Console.WriteLine("Passenger Name: [Enter ~ to cancel operation]");
                userInput = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Invalid Input\n");
                    continue;
                }
                else if (userInput != "~")
                {
                    name = userInput;
                }
                else
                {
                    return;
                }
            } while (String.IsNullOrWhiteSpace(name));
            //Create passenger
            var passenger = new Passenger(name);
            PassengerService.AddPassenger(passenger);
            Console.WriteLine("\nPassenger Information Added successfully\n");
            Console.WriteLine(passenger);
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }
        internal static void SearchForAvailableFlights()
        {
            String userInput = String.Empty;
            Dictionary<String, Object> props = new();
            do
            {
                Console.Clear();
                Console.WriteLine("### Search for Available Flights ###");
                Console.WriteLine("## Please select the criteria you want to add for the search ##");
                Console.WriteLine("Enter 1: Set a specific value for the [ID] to be included in the criteria.");
                Console.WriteLine("Enter 2: Set a specific value for the [Departure Country] to be included in the criteria.");
                Console.WriteLine("Enter 3: Set a specific value for the [Destination Country] to be included in the criteria.");
                Console.WriteLine("Enter 4: Set a specific value for the [Departure Date] to be included in the criteria.");
                Console.WriteLine("Enter 5: Set a specific value for the [Departure Airport] to be included in the criteria.");
                Console.WriteLine("Enter 6: Set a specific value for the [Arrival Airport] to be included in the criteria.");
                Console.WriteLine("Enter 7: Set a specific value for the [Class] to be included in the criteria.");
                Console.WriteLine("Enter 8: Set a specific value for the [Price] to be included in the criteria.");
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
                        String _Id = Console.ReadLine();
                        bool success = int.TryParse(_Id, out int id);
                        if (!success) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["Id"] = id;
                        break;
                    case "2":
                        Console.WriteLine("Enter the Departure Country: ");
                        String _departureCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureCountry)) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["DepartureCountry"] = _departureCountry;
                        break;                   
                    case "3":
                        Console.WriteLine("Enter the Destination Country: ");
                        String _destinationCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_destinationCountry)) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["DestinationCountry"] = _destinationCountry;
                        break;                    
                    case "4":
                        Console.WriteLine("Enter the Departure Date: ");
                        String _departureDate = Console.ReadLine();
                        bool _success = DateTime.TryParse(_departureDate, out DateTime departureDate);
                        if (!_success) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["DepartureDate"] = departureDate;
                        break;
                    case "5":
                        Console.WriteLine("Enter the Departure Airport: ");
                        String _departureAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureAirport)) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["DepartureAirport"] = _departureAirport;
                        break;                    
                    case "6":
                        Console.WriteLine("Enter the Arrival Airport: ");
                        String _arrivalAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_arrivalAirport)) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["ArrivalAirport"] = _arrivalAirport;
                        break;
                    case "7":
                        Console.WriteLine("Choose one of these Classes: ");
                        Console.WriteLine("Enter 1: Economy seats");
                        Console.WriteLine("Enter 2: Business seats");
                        Console.WriteLine("Enter 3: FirstClass seats");
                        String selected = Console.ReadLine();
                        switch (selected) {
                            case "1": props["Class"] = Seat.Economy;break;
                            case "2": props["Class"] = Seat.Business;break;
                            case "3": props["Class"] = Seat.FirstClass;break;
                            default: Console.WriteLine("invalid input"); Console.ReadLine(); break;
                        }
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;

                }
            } while (userInput != "0");
            var flights = FlightService.Search(props);
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
                Console.WriteLine();
            }
        }
    }
}
