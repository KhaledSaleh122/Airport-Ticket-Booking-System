using AirportTicketBookingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Services
{
    internal static class BookService
    {
        private static Dictionary<int,Book> books = [];
        public static void addBook(Book book) {
            book.BookedFlight.AddPassengerToFlight(book.Class);
            books.Add(book.Id, book);
        }
        public static void removeBook(Book book)
        {
            book.BookedFlight.RemovePassengerFromFlight(book.Class);
            books.Remove(book.Id);
        }

        public static List<Book> getBooks() { 
            return books.Values.ToList();
        }         
        public static List<Book> getBooks(Passenger passenger) {
            return books.Select(book=> book.Value).Where(book => book.Passenger == passenger).ToList();
        }
        public static Book? getBook(Passenger passenger,int id)
        {
            return books.FirstOrDefault(book => book.Value.Passenger == passenger && book.Key == id).Value;
        }
    }
}
