using AirportTicketBookingSystem.Domain.Classes.Booking;
using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Passegner;
using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
namespace AirportTicketBookingSystem_Test
{
    public class BookingClassTestShould
    {
        private readonly Fixture _fixture;

        public BookingClassTestShould()
        {
            _fixture = new Fixture();
        }
        [Fact]
        public void ReturnAbooking()
        {
            _fixture.Inject<Dictionary<SeatClasses, AbstractSeat>>(new(){
                { SeatClasses.Economy, new EconomySeat() },
                { SeatClasses.Business, new BusinessSeat() },
                { SeatClasses.FirstClass, new FirstClassSeat() }
            });
            var flight = _fixture.Create<StandardFlight>();
            var passenger = _fixture.Create<Passenger>();
            var selectedClass = SeatClasses.Economy;
            var booking = new Booking(flight, passenger, selectedClass);

            booking.Passenger.Should().Be(passenger);
            booking.BookedFlight.Should().Be(flight);
            booking.Class.Should().Be(selectedClass);
            booking.Price.Should().Be(flight.seats[selectedClass].SeatPrice);
        }

    }
}
