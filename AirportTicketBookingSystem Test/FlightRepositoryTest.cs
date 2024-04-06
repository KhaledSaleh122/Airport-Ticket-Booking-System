using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Models;
using AutoFixture;
using FluentAssertions;

namespace AirportTicketBookingSystem_Test
{
    public class FlightRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly SeatClasses _selectedClass;
        public FlightRepositoryTest()
        {
            _fixture = new Fixture();
            _fixture.Inject<Dictionary<SeatClasses, AbstractSeat>>(new(){
                { SeatClasses.Economy, new EconomySeat() },
                { SeatClasses.Business, new BusinessSeat() },
                { SeatClasses.FirstClass, new FirstClassSeat() }
            });
            _selectedClass = SeatClasses.Economy;
        }
        [Fact]
        public void ShouldAddFlight()
        {
            var flight = _fixture.Create<StandardFlight>();
            FlightRepository.AddFlight(flight);
            FlightRepository.GetFlights().Values.Should().Contain(flight);
        }
        [Fact]
        public void ShouldReturnAddedFlights()
        {
            var flight = _fixture.Create<StandardFlight>();
            var flight1 = _fixture.Create<StandardFlight>();
            var flight2 = _fixture.Create<StandardFlight>();
            FlightRepository.AddFlight(flight);
            FlightRepository.AddFlight(flight1);
            FlightRepository.AddFlight(flight2);
            FlightRepository.GetFlights().Values.Should().Contain([flight, flight1, flight2]);
        }
        [Fact]
        public void ShouldReturnMatchesFlights()
        {
            var flight = _fixture.Create<StandardFlight>();
            FlightRepository.AddFlight(flight);
            var searchFlight = new SearchFlightModel(
                    Id: flight.Id
                );
            FlightRepository.FindMatchesFlight(searchFlight).Should().OnlyContain((x) => x.Id == flight.Id);
        }

        [Fact]
        public void ShouldReturnAvailableFlights()
        {
            var flight = _fixture.Create<StandardFlight>();
            FlightRepository.AddFlight(flight);
            FlightRepository.FindAvailableFlights(new SearchFlightModel()).Should().OnlyContain((x) => x.DepartureDate >= DateTime.Now);
        }
    }
}
