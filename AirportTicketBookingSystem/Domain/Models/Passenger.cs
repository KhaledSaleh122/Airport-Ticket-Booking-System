using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Models
{
    internal class Passenger
    {
        private static int id = 1;
        private String name = String.Empty;
        public required int Id { get; init; }
        public required string Name
        {
            get => name;
            init
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                name = value;
            }
        }
        [SetsRequiredMembers]
        public Passenger(string name)
        {
            Id = id++;
            Name = name;
        }

        public override string ToString() => $"PassngerInfo:\nID: {Id}\nName: {Name}";
    }
}
