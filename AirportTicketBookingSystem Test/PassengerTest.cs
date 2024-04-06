using AirportTicketBookingSystem.Domain.Classes.Passegner;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem_Test
{
    public class PassengerTest
    {
        private readonly Fixture _fixture;

        public PassengerTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldAddPassenger()
        {
            var passenger = _fixture.Create<Passenger>();
            PassengerService.AddPassenger(passenger);
            PassengerService.GetPassengers().Should().Contain(passenger);
        }

        [Fact]
        public void ShouldReturnPassengerIfMatched()
        {
            var passenger = _fixture.Create<Passenger>();
            PassengerService.AddPassenger(passenger);
            PassengerService.GetPassenger(passenger.Id).Should().Be(passenger);
        }

        [Fact]
        public void ShouldReturnNullIfPassengerNotMatched()
        {
            PassengerService.GetPassenger(-1).Should().Be(null);
        }
    }
}
