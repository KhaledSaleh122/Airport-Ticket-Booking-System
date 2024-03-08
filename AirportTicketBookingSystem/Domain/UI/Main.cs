using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.UI
{
    internal static class Main
    {
        public static void ShowMainMenu() {
            String userInput = String.Empty;
            do {
                Console.Clear();
                Console.WriteLine("####################");
                Console.WriteLine("## Main Menu ##");
                Console.WriteLine("####################");
                Console.WriteLine();
                Console.WriteLine("## Your Selection ##");
                Console.WriteLine("Enter 1: Passenger");
                Console.WriteLine("Enter 2: Manager");
                Console.WriteLine("Enter 0: To Exist");
                Console.WriteLine();
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        PassengerMenu();
                        break;
                    case "2":
                        //BookFlight(passenger);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("invalid input");
                        Console.WriteLine("\nPress enter to back");
                        Console.ReadLine();
                        break;
                }
            }while (true);
        }
        static void PassengerMenu()
        {
            String userInput = String.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("####################");
                Console.WriteLine("## Passenger Main Menu ##");
                Console.WriteLine("####################");
                Console.WriteLine();
                Console.WriteLine("## Your Selection ##");
                Console.WriteLine("Enter 1: Signin Passenger Account");
                Console.WriteLine("Enter 2: Create Passenger Account");
                Console.WriteLine("Enter 0: To Exist");
                Console.WriteLine();
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        PassengerProgram.UseCreatedPassengerAccount();
                        break;
                    case "2":
                        PassengerProgram.AddPassenger();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("invalid input");
                        Console.WriteLine("\nPress enter to back");
                        Console.ReadLine();
                        break;
                }
            } while (true);
        }

    }
}
