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
        private static Dictionary<int,Book> books = [];
        public static void AddBook(Book book) {
            book.BookedFlight.AddPassengerToFlight(book.Class);
            books.Add(book.Id, book);
        }
        internal static void RemoveBook(Book book)
        {
            book.BookedFlight.RemovePassengerFromFlight(book.Class);
            books.Remove(book.Id);
        }

        internal static List<Book> GetBooks() { 
            return books.Values.ToList();
        }
        internal static List<Book> GetBooks(Passenger passenger) {
            return books.Select(book=> book.Value).Where(book => book.Passenger == passenger).ToList();
        }
        internal static Book? GetBook(Passenger passenger,int id)
        {
            return books.FirstOrDefault(book => book.Value.Passenger == passenger && book.Key == id).Value;
        }
        internal static bool PassengerAlreadyBookedThis(Passenger passenger,Flight flight) {
            return books.Count(book => book.Value.Passenger == passenger && book.Value.BookedFlight == flight) > 0 ?true:false;
        }
        internal static List<Book> Filter(Dictionary<String, Object>? criterias) {
            return books.Select((book) => book.Value).Where((value) => FilterMatch(value, criterias)).ToList();
        }
        private static bool FilterMatch(Book book,Dictionary<String,Object>? criterias) {
            if (criterias is null) return true;
            foreach (var criteria in criterias) {
                if (criteria.Value is null) continue;
                if (criteria.Key == "PassengerId")
                {
                    if (criteria.Value.GetType() == typeof(int) && (int)criteria.Value != book.Passenger.Id) return false;
                }
                else if (criteria.Key == "FlightId") {
                    if (criteria.Value.GetType() == typeof(int) && (int)criteria.Value != book.BookedFlight.Id) return false;
                }
                else
                {
                    var bookProperty = book.GetType().GetProperty(criteria.Key);
                    if (bookProperty is null)
                    {
                        if (!FlightService.Match(book.BookedFlight, criteria, criterias)) return false;
                    }
                    else
                    {
                        if (!object.Equals(criteria.Value, bookProperty.GetValue(book))) return false;
                    }
                }
            }
            return true;
        }
    }
}
