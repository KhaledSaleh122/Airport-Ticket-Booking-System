using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Domain.Classes.Passegner
{
    public class Passenger
    {
        private static int id = 1;
        private string name = string.Empty;
        public int Id { get; }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public Passenger(string name)
        {
            Id = id++;
            Name = name;
        }

        public override string ToString() => $"PassngerInfo:\n\nID: {Id}\nName: {Name}";
    }
}
