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
            books.Add(book.Id, book);
        }
        public static void removeBook(Book book)
        {
            books.Remove(book.Id);
        }

        public static Dictionary<int, Book> getBooks() { 
            return books.ToDictionary();
        } 
    }
}
