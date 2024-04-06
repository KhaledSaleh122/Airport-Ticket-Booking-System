using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Passegner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace AirportTicketBookingSystem.Domain.Classes.Booking
{
    public static class BookService
    {
        private readonly static Dictionary<int, Booking> _bookings = [];
        public static void AddBook(Booking book)
        {
            book.BookedFlight.AddPassengerToFlight(book.Class);
            _bookings.Add(book.Id, book);
        }
        public static void RemoveBook(Booking book)
        {
            book.BookedFlight.RemovePassengerFromFlight(book.Class);
            _bookings.Remove(book.Id);
        }

        public static List<Booking> GetBooks()
        {
            return [.. _bookings.Values];
        }
        public static List<Booking> GetBooks(Passenger passenger)
        {
            return _bookings.Select(book => book.Value).Where(book => book.Passenger == passenger).ToList();
        }
        public static Booking? GetBook(Passenger passenger, int id)
        {
            return _bookings.FirstOrDefault(book => book.Value.Passenger == passenger && book.Key == id).Value;
        }
        public static bool PassengerAlreadyBookedThis(Passenger passenger, AbstractFlight flight)
        {
            return _bookings.Count(book => book.Value.Passenger == passenger && book.Value.BookedFlight == flight) > 0 ? true : false;
        }

        public static List<Booking> FindMatchesBooks(SearchBookingModel searchBookingModel)
        {
            return _bookings.Select((bookKeyValue) => bookKeyValue.Value).Where((book) => searchBookingModel.Match(book)).ToList();
        }
    }
}
