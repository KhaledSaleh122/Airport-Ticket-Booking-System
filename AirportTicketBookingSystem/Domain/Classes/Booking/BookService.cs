using AirportTicketBookingSystem.Domain.Classes.Booking;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace AirportTicketBookingSystem.Domain.Services
{
    internal static class BookService
    {
        private readonly static Dictionary<int, Book> _books = [];
        public static void AddBook(Book book)
        {
            book.BookedFlight.AddPassengerToFlight(book.Class);
            _books.Add(book.Id, book);
        }
        internal static void RemoveBook(Book book)
        {
            book.BookedFlight.RemovePassengerFromFlight(book.Class);
            _books.Remove(book.Id);
        }

        internal static List<Book> GetBooks()
        {
            return [.. _books.Values];
        }
        internal static List<Book> GetBooks(Passenger passenger)
        {
            return _books.Select(book => book.Value).Where(book => book.Passenger == passenger).ToList();
        }
        internal static Book? GetBook(Passenger passenger, int id)
        {
            return _books.FirstOrDefault(book => book.Value.Passenger == passenger && book.Key == id).Value;
        }
        internal static bool PassengerAlreadyBookedThis(Passenger passenger, Flight flight)
        {
            return _books.Count(book => book.Value.Passenger == passenger && book.Value.BookedFlight == flight) > 0 ? true : false;
        }

        internal static List<Book> FindMatchesBooks(SearchBookingModel searchBookingModel)
        {
            return _books.Select((bookKeyValue) => bookKeyValue.Value).Where((book) => searchBookingModel.Match(book)).ToList();
        }
    }
}
