using AirportTicketBookingSystem.Domain.Classes.Flight;
using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AutoFixture;
using FluentAssertions;

namespace AirportTicketBookingSystem_Test
{
    public class TestData()
    {
        public static IEnumerable<Object[]> SeatClassesTestData
        {
            get
            {
                yield return new object[] { SeatClasses.Economy };
                yield return new object[] { SeatClasses.Business };
                yield return new object[] { SeatClasses.FirstClass };
            }
        }
    }
    public class StandardFlightTest
    {
        private readonly Fixture _fixture;
        public StandardFlightTest()
        {
            _fixture = new Fixture();
            _fixture.Inject<Dictionary<SeatClasses, AbstractSeat>>(new(){
                { SeatClasses.Economy, new EconomySeat() },
                { SeatClasses.Business, new BusinessSeat() },
                { SeatClasses.FirstClass, new FirstClassSeat() }
            });
        }

        [Theory]
        [MemberData(nameof(TestData.SeatClassesTestData), MemberType = typeof(TestData))]
        public void ShouldAddPassengerToFlight(SeatClasses selectedClass)
        {
            var sut = _fixture.Create<StandardFlight>();
            var availableSeats = sut.seats[selectedClass].AvailableSeats;
            sut.AddPassengerToFlight(selectedClass);
            if (sut.IsThereAvailableSeat(selectedClass))
            {
                sut.seats[selectedClass].AvailableSeats.Should().Be(availableSeats - 1);
            }
            else
            {
                sut.seats[selectedClass].AvailableSeats.Should().Be(sut.seats[selectedClass].MaxSeats);
            }
        }
        [Theory]
        [InlineData(SeatClasses.Economy, 0)]
        [InlineData(SeatClasses.Business, 10)]
        public void ShouldReturnIfThereAvailabelSeats(SeatClasses selectedClass, int availableSeats)
        {
            var sut = _fixture.Create<StandardFlight>();
            sut.seats[selectedClass].AvailableSeats = availableSeats;
            if (sut.seats[selectedClass].AvailableSeats > 0)
            {
                sut.IsThereAvailableSeat(selectedClass).Should().BeTrue();
            }
            else
            {
                sut.IsThereAvailableSeat(selectedClass).Should().BeFalse();
            }
        }
        [Theory]
        [MemberData(nameof(TestData.SeatClassesTestData), MemberType = typeof(TestData))]
        public void ShouldRemovePassengerFromFlight(SeatClasses selectedClass)
        {
            var sut = _fixture.Create<StandardFlight>();
            var availableSeats = sut.seats[selectedClass].AvailableSeats;
            var maxSeats = sut.seats[selectedClass].MaxSeats;
            sut.RemovePassengerFromFlight(selectedClass);
            if (availableSeats < maxSeats)
            {
                sut.seats[selectedClass].AvailableSeats.Should().Be(availableSeats + 1);
            }
            else
            {
                sut.seats[selectedClass].AvailableSeats.Should().Be(maxSeats);
            }
        }
    }
}
