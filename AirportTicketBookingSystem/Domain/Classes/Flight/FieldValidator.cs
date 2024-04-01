using AirportTicketBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    internal class FieldValidatorModel
    {

        public string Name { get; set; }
        public bool Required { get; set; }
        public string AddtionErrorMessage { get; set; }
        public Predicate<object> Predicate { get; set; }
        public object? CurrValue { get; set; }
        public Type DataType { get; set; }
        public FieldValidatorModel(Type dataType, string name, bool required, Predicate<object> predicate, string addtionErrorMessage = "")
        {
            DataType = dataType;
            Name = name;
            Required = required;
            Predicate = predicate;
            AddtionErrorMessage = addtionErrorMessage;
        }
    }
}
