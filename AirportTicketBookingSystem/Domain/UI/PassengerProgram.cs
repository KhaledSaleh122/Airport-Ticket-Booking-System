using AirportTicketBookingSystem.Domain.Classes.Booking;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Passegner;
using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;


namespace AirportTicketBookingSystem.Domain.UI
{
    internal static class PassengerProgram
    {
        internal static void UseCreatedPassengerAccount()
        {
            String? userInput = String.Empty;
            Console.Clear();
            Console.WriteLine("## Signin Passenger Account ##");
            Passenger? passenger = null;
            do
            {
                Console.WriteLine("\n\nEnter the passenger account Id: [Enter ~ to cancel operation]");
                userInput = Console.ReadLine();
                if (userInput == "~") return;
                bool success = int.TryParse(userInput, out int id);
                if (!success) { Console.WriteLine("invalid input"); Console.ReadLine(); continue; }
                passenger = PassengerService.GetPassenger(id);
                if (passenger is null) { Console.WriteLine("Couldn't find the passenger account"); continue; }
            } while (passenger is null);
            ShowPassengerMenu(passenger);
        }
        internal static void AddPassenger()
        {
            String? userInput = String.Empty;
            String? name = String.Empty;
            Console.Clear();
            Console.WriteLine("##Create Passenger Account##");
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
            ShowPassengerMenu(passenger);
        }
        internal static void ShowPassengerMenu(Passenger passenger)
        {
            String? userInput = String.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("####################");
                Console.WriteLine("## Passenger Menu ##");
                Console.WriteLine("####################");
                Console.WriteLine();
                Console.WriteLine(passenger);
                Console.WriteLine();
                Console.WriteLine("## Your Selection ##");
                Console.WriteLine("Enter 1: Search for Available Flights");
                Console.WriteLine("Enter 2: Book a Flight");
                Console.WriteLine("Enter 3: Cancel a booking");
                Console.WriteLine("Enter 4: Modify a booking");
                Console.WriteLine("Enter 5: View personal bookings");
                Console.WriteLine("Enter 0: To Exist");
                Console.WriteLine();
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        SearchForAvailableFlights();
                        break;
                    case "2":
                        BookFlight(passenger);
                        break;
                    case "3":
                        CancelBooking(passenger);
                        break;
                    case "4":
                        ModifyBooking(passenger);
                        break;
                    case "5":
                        ViewPersonalBooking(passenger);
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
        internal static void SearchForAvailableFlights()
        {
            String? userInput = String.Empty;
            var props = new Dictionary<String, Object?>();
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
                        String? _Id = Console.ReadLine();
                        bool success = int.TryParse(_Id, out int id);
                        if (!success) { Console.WriteLine("invalid input"); Console.ReadLine(); break; }
                        props["Id"] = id;
                        break;
                    case "2":
                        Console.WriteLine("Enter the Departure Country: ");
                        String? _departureCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureCountry)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureCountry"] = _departureCountry;
                        break;
                    case "3":
                        Console.WriteLine("Enter the Destination Country: ");
                        String? _destinationCountry = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_destinationCountry)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DestinationCountry"] = _destinationCountry;
                        break;
                    case "4":
                        Console.WriteLine("Enter the Departure Date: ");
                        String? _departureDate = Console.ReadLine();
                        bool _success = DateTime.TryParse(_departureDate, out DateTime departureDate);
                        if (!_success) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureDate"] = departureDate;
                        break;
                    case "5":
                        Console.WriteLine("Enter the Departure Airport: ");
                        String? _departureAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_departureAirport)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["DepartureAirport"] = _departureAirport;
                        break;
                    case "6":
                        Console.WriteLine("Enter the Arrival Airport: ");
                        String? _arrivalAirport = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(_arrivalAirport)) { Console.WriteLine("invalid input"); Console.WriteLine("\nPress enter to back"); Console.ReadLine(); break; }
                        props["ArrivalAirport"] = _arrivalAirport;
                        break;
                    case "7":
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
                    case "8":
                        Console.WriteLine("Enter the price: ");
                        String? _price = Console.ReadLine();
                        bool __success = decimal.TryParse(_price, out decimal price);
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
            var criterias = new SearchFlightModel(
                    (int?)props.GetValueOrDefault("Id"),
                    (String?)props.GetValueOrDefault("DepartureCountry"),
                    (String?)props.GetValueOrDefault("DestinationCountry"),
                    (DateTime?)props.GetValueOrDefault("DepartureDate"),
                    (String?)props.GetValueOrDefault("DepartureAirport"),
                    (String?)props.GetValueOrDefault("ArrivalAirport"),
                    (props.GetValueOrDefault("Class") is null ? default : (SeatClasses)props.GetValueOrDefault("Class")!),
                    (decimal?)props.GetValueOrDefault("Price")
                );
            var flights = FlightRepository.FindAvailableFlights(criterias);
            if (flights.Count == 0)
            {
                Console.WriteLine("No data match your criteria.");
            }
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
                Console.WriteLine();
            }
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }
        internal static void BookFlight(Passenger passenger)
        {
            String? userInput = String.Empty;
            Console.WriteLine("### Book a Flight ###");
            AbstractFlight? flight = null;
            do
            {
                Console.WriteLine("\nEnter Id of the flight you want to book: [Enter ~ to cancel operation]");
                userInput = Console.ReadLine();
                if (userInput == "~") return;
                bool sucess = int.TryParse(userInput, out int id);
                if (!sucess) { Console.WriteLine("invalid input"); continue; }
                List<AbstractFlight> targetFlight = FlightRepository.FindAvailableFlights(new SearchFlightModel(id));
                if (targetFlight.Count == 0) { Console.WriteLine("Selected Flgiht Not available"); continue; }
                flight = targetFlight[0];
                if (BookService.PassengerAlreadyBookedThis(passenger, flight))
                {
                    Console.WriteLine("You already booked this");
                    continue;
                }
                break;
            } while (true);
            Console.WriteLine();
            Console.WriteLine(flight);
            SeatClasses seat;
            do
            {
                Console.WriteLine("\nChoose Seat Class: ");
                Console.WriteLine("Enter 1: Economy seats");
                Console.WriteLine("Enter 2: Business seats");
                Console.WriteLine("Enter 3: FirstClass seats");
                Console.WriteLine("Enter 4: To cancel operation");
                String? selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        if (!flight.IsThereAvailableSeat(SeatClasses.Economy)) { Console.WriteLine("No available seat for this class"); continue; }
                        seat = SeatClasses.Economy;
                        break;
                    case "2":
                        if (!flight.IsThereAvailableSeat(SeatClasses.Business)) { Console.WriteLine("No available seat for this class"); continue; }
                        seat = SeatClasses.Business; break;
                    case "3":
                        if (!flight.IsThereAvailableSeat(SeatClasses.FirstClass)) { Console.WriteLine("No available seat for this class"); continue; }
                        seat = SeatClasses.FirstClass; break;
                    case "4": return;
                    default: Console.WriteLine("invalid input"); continue;
                }
                break;
            } while (true);
            Book book = new(flight, passenger, seat);
            BookService.AddBook(book);
            Console.WriteLine("\nYour flight reservation has been confirmed\n");
            Console.WriteLine(book);
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }
        internal static void ViewPersonalBooking(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine("###Current Books###");
            List<Book> books = BookService.GetBooks(passenger);
            if (books.Count == 0)
            {
                Console.WriteLine("\nNo Data yet!");
            }
            foreach (var book in books)
            {
                Console.WriteLine($"{book}\n\n////////////////////////////////\n\n");
            }
            Console.WriteLine("\n\nPress enter to back");
            Console.ReadLine();
        }

        internal static void CancelBooking(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine("###Cancel Book###");
            String? userInput = String.Empty;
            Book? book = null;
            do
            {
                Console.WriteLine("\nEnter Id of the book you want to cancel: [Enter ~ to cancel operation]");
                userInput = Console.ReadLine();
                if (userInput == "~") return;
                bool sucess = int.TryParse(userInput, out int id);
                if (!sucess) { Console.WriteLine("invalid input"); continue; }
                book = BookService.GetBook(passenger, id);
                if (book == null) { Console.WriteLine("Couldn't find the Book"); continue; }
                break;
            } while (true);
            BookService.RemoveBook(book);
            Console.WriteLine("\nYour flight reservation has been Canceled\n");
            Console.WriteLine(book);
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }

        internal static void ModifyBooking(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine("###Modify Book###");
            String? userInput = String.Empty;
            Book? book = null;
            do
            {
                Console.WriteLine("\nEnter Id of the book you want to cancel: [Enter ~ to cancel operation]");
                userInput = Console.ReadLine();
                if (userInput == "~") return;
                bool sucess = int.TryParse(userInput, out int id);
                if (!sucess) { Console.WriteLine("invalid input"); continue; }
                book = BookService.GetBook(passenger, id);
                if (book == null) { Console.WriteLine("Couldn't find the Book"); continue; }
                break;
            } while (true);
            do
            {
                Console.WriteLine("\nChoose Seat Class: ");
                Console.WriteLine("Enter 1: Economy seats");
                Console.WriteLine("Enter 2: Business seats");
                Console.WriteLine("Enter 3: FirstClass seats");
                Console.WriteLine("Enter 4: To cancel operation");
                String? selected = Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        if (book.Class == SeatClasses.Economy) { break; }
                        if (!book.BookedFlight.IsThereAvailableSeat(SeatClasses.Economy)) { Console.WriteLine("No available seat for this class"); continue; }
                        book.Class = SeatClasses.Economy;
                        book.Price = book.BookedFlight.seats[book.Class].SeatPrice;
                        break;
                    case "2":
                        if (book.Class == SeatClasses.Business) { break; }
                        if (!book.BookedFlight.IsThereAvailableSeat(SeatClasses.Business)) { Console.WriteLine("No available seat for this class"); continue; }
                        book.Class = SeatClasses.Business;
                        book.Price = book.BookedFlight.seats[book.Class].SeatPrice;
                        break;
                    case "3":
                        if (book.Class == SeatClasses.FirstClass) { break; }
                        if (!book.BookedFlight.IsThereAvailableSeat(SeatClasses.FirstClass)) { Console.WriteLine("No available seat for this class"); continue; }
                        book.Class = SeatClasses.FirstClass;
                        book.Price = book.BookedFlight.seats[book.Class].SeatPrice;
                        break;
                    case "4": return;
                    default: Console.WriteLine("invalid input"); continue;
                }
                break;
            } while (true);
            Console.WriteLine("\nYour modified the book succesfully\n");
            Console.WriteLine(book);
            Console.WriteLine("\nPress enter to back");
            Console.ReadLine();
        }
    }
}
