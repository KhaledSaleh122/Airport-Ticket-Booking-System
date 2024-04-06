using AirportTicketBookingSystem.Domain.Classes.Booking;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Passegner;
using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem_Test
{
    public class BookingServiceTest
    {
        private readonly Fixture _fixture;
        private readonly SeatClasses _selectedClass;

        public BookingServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Inject<Dictionary<SeatClasses, AbstractSeat>>(new(){
                { SeatClasses.Economy, new EconomySeat() },
                { SeatClasses.Business, new BusinessSeat() },
                { SeatClasses.FirstClass, new FirstClassSeat() }
            });
            var flight = _fixture.Create<StandardFlight>();
            var passenger = _fixture.Create<Passenger>();
            _selectedClass = SeatClasses.Economy;
            _fixture.Inject(_selectedClass);
            _fixture.Inject<AbstractFlight>(flight);
        }

        [Fact]
        public void ShouldAddBooking()
        {
            var booking = _fixture.Create<Booking>();
            BookService.AddBook(booking);
            BookService.GetBooks().Should().Contain(booking);

        }
        [Fact]
        public void ShouldRemoveAvabliableSeatAfterAddingBooking()
        {
            var booking = _fixture.Create<Booking>();
            var avaliableSeats = booking.BookedFlight.seats[_selectedClass].AvailableSeats;
            BookService.AddBook(booking);
            booking.BookedFlight.seats[_selectedClass].AvailableSeats.Should().Be(
                    avaliableSeats - 1
                );
        }

        [Fact]
        public void ShouldRemoveBooking()
        {
            var booking = _fixture.Create<Booking>();
            BookService.RemoveBook(booking);
            BookService.GetBooks().Should().NotContain(booking);

        }

        [Fact]
        public void ShouldAddAvabliableSeatAfterRemovingBooking()
        {
            var booking = _fixture.Create<Booking>();
            BookService.AddBook(booking);
            var avaliableSeats = booking.BookedFlight.seats[_selectedClass].AvailableSeats;
            BookService.RemoveBook(booking);
            booking.BookedFlight.seats[_selectedClass].AvailableSeats.Should().Be(
                    avaliableSeats + 1
                );
        }

        [Fact]
        public void ShouldReturnAddedBooking()
        {
            var booking = _fixture.Create<Booking>();
            var booking1 = _fixture.Create<Booking>();
            var booking2 = _fixture.Create<Booking>();
            BookService.AddBook(booking);
            BookService.AddBook(booking1);
            BookService.AddBook(booking2);
            BookService.GetBooks().Should().Contain([booking, booking1, booking2]);
        }

        [Fact]
        public void ShouldReturnBookingForSpecificPassenger()
        {
            var passenger = _fixture.Create<Passenger>();
            var passenger1 = _fixture.Create<Passenger>();
            var flight = _fixture.Create<StandardFlight>();
            var booking = new Booking(flight, passenger, _selectedClass);
            var booking1 = new Booking(flight, passenger1, _selectedClass);
            BookService.AddBook(booking);
            BookService.AddBook(booking1);
            BookService.GetBooks(passenger).Should().Contain(booking);
            BookService.GetBooks(passenger).Should().NotContain(booking1);
        }
        [Fact]
        public void ShouldReturnIfPassengerAlreadyBookedSameFlight()
        {
            var passenger = _fixture.Create<Passenger>();
            var flight = _fixture.Create<StandardFlight>();
            var booking = new Booking(flight, passenger, _selectedClass);
            BookService.AddBook(booking);
            BookService.PassengerAlreadyBookedThis(passenger, flight).Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnMatchedBookings()
        {
            var flight = _fixture.Create<StandardFlight>();
            var passenger = _fixture.Create<Passenger>();
            var booking = new Booking(flight, passenger, _selectedClass);
            var searchBooking = new SearchBookingModel(
                Id: booking.Id
            );
            BookService.AddBook(booking);
            BookService.FindMatchesBooks(searchBooking).Should().Contain(booking);
        }
    }
}
