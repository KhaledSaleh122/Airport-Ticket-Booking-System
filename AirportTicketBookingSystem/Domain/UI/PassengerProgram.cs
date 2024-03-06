using AirportTicketBookingSystem.Domain.Models;
using AirportTicketBookingSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
