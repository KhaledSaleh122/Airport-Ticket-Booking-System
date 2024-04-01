using AirportTicketBookingSystem.Domain.Classes.Seat;
using AirportTicketBookingSystem.Domain.Enums;
using AirportTicketBookingSystem.Domain.Interfaces;
using Microsoft.VisualBasic.FileIO;
namespace AirportTicketBookingSystem.Domain.Classes.Flight
{
    public class CsvFlightDataSource : IFlightDataSource
    {
        private readonly List<string> _errors = [];
        private readonly string _filePath;
        private readonly List<AbstractFlight> _flights = [];
        private int _line = 1;
        public CsvFlightDataSource(string filePath)
        {
            _filePath = filePath;
        }
        public List<string> LoadFlightsFromSource()
        {
            using var parser = new TextFieldParser(_filePath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                string[]? fields;
                try
                {
                    fields = parser.ReadFields();
                }
                catch
                {
                    throw new Exception("Error while reading file");
                }
                if (fields is null) continue;
                if (_line == 1) { _line++; continue; } // escape first line, in first line only columns name
                if (VerifyFieldsData(fields))
                {
                    AddVerifiedFlightToList();
                }
                _line++;
            }
            if (_errors.Count == 0)
            {
                foreach (var flight in _flights)
                {
                    FlightRepository.AddFlight(flight);
                }
            }
            return _errors;
        }
        private void AddVerifiedFlightToList()
        {
            object currncy = FlightValidators.Validators[5].CurrValue ?? Currency.USD;
            object economyMaxSeats = FlightValidators.Validators[6].CurrValue ?? 50;
            object economyPricePerSeat = FlightValidators.Validators[7].CurrValue ?? 150;
            object BusinessMaxSeats = FlightValidators.Validators[8].CurrValue ?? 30;
            object BusinessPricePerSeat = FlightValidators.Validators[9].CurrValue ?? 750;
            object FirstClassMaxSeats = FlightValidators.Validators[10].CurrValue ?? 10;
            object FirstClassPricePerSeat = FlightValidators.Validators[11].CurrValue ?? 1500;
            AbstractFlight flight = new StandardFlight(
                (string)FlightValidators.Validators[0].CurrValue!,
                (string)FlightValidators.Validators[1].CurrValue!,
                (DateTime)FlightValidators.Validators[2].CurrValue!,
                (string)FlightValidators.Validators[3].CurrValue!,
                (string)FlightValidators.Validators[4].CurrValue!
                )
            {
                Currency = (Currency)currncy,
            };
            flight.seats[SeatClasses.Economy] = new EconomySeat((decimal)economyPricePerSeat, (int)economyMaxSeats);
            flight.seats[SeatClasses.Business] = new BusinessSeat((decimal)BusinessPricePerSeat, (int)BusinessMaxSeats);
            flight.seats[SeatClasses.FirstClass] = new FirstClassSeat((decimal)FirstClassPricePerSeat, (int)FirstClassMaxSeats);
            _flights.Add(flight);
        }
        private bool VerifyFieldsData(string[] fields)
        {
            bool isVaild = false;
            for (int i = 0; i < FlightValidators.Validators.Count; i++)
            {
                isVaild = false;
                var fieldData = FlightValidators.Validators[i];
                string fieldAdditionMessage = fieldData.AddtionErrorMessage;
                string fieldName = fieldData.Name;
                bool required = fieldData.Required;
                if (IsOutOfIndex(i, fields))
                {
                    if (required)
                    {
                        AddErrorMessage(fieldName, fieldAdditionMessage, "Unknown");
                    }
                    continue;
                }

                object fieldValue;
                try
                {
                    fieldValue = GetConvertedfieldValue(fields, i, fieldData);
                }
                catch
                {
                    AddErrorMessage(fieldName, fieldAdditionMessage, fields[i]);
                    continue;
                }

                if (!fieldData.Predicate(fieldValue))
                {
                    AddErrorMessage(fieldName, fieldAdditionMessage, fields[i]);
                    continue;
                }
                fieldData.CurrValue = fieldValue;
                isVaild = true;
            }
            return isVaild;
        }
        private static object GetConvertedfieldValue(string[] fields, int index, FieldValidatorModel fieldData)
        {
            if (fieldData.DataType == typeof(Currency))
            {
                if (Enum.TryParse(typeof(Currency), fields[index], out object? currencyValue) && currencyValue is not null)
                {
                    return currencyValue;
                }
                throw new Exception("Invaild input");
            }
            else
            {
                return Convert.ChangeType(fields[index], fieldData.DataType);
            }
        }
        private void AddErrorMessage(string name, string? addtionErrorMessage, string inputValue)
        {

            _errors.Add(
                   $"""
                    *{name}:
                        - Line: {_line}
                        - Input-Value: {inputValue}
                        - Type: String
                        - Constraint: Required{(addtionErrorMessage is null ? "" : $", {addtionErrorMessage}")}
                    """
            );
        }
        private static bool IsOutOfIndex(int index, string[] fields)
        {
            return index > fields.Length - 1;
        }
    }
}
