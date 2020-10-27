using System;
using System.Globalization;

namespace ImperialUnitConversion
{
    class ImperialLengthConverter : UnitConverter<ImperialLengthUnits, float>
    {
        public static string Convert(string input)
        {
            try
            {
                var inputSplit = input.Trim().Split(" ");

                if (inputSplit.Length != 4)
                    return
                        "ERROR: The conversion format is not correct. it should look like this: [number] [FromUnit] in [ToUnit].";

                if (inputSplit[2].ToLower() != "in")
                    return "ERROR: The \"in\" keyword is missing.";

                if (!float.TryParse(inputSplit[0], NumberStyles.Float, CultureInfo.CurrentCulture, out var value))
                    return "ERROR: The number is not in a correct format.";

                var fromEnumUnit = GetUnit(inputSplit[1]);
                var toEnumUnit = GetUnit(inputSplit[3]);

                static ImperialLengthUnits GetUnit(string unit)
                {
                    return !Enum.TryParse(unit, true, out ImperialLengthUnits enumUnit) &&
                           !TryGetUnit(unit, out enumUnit)
                        ? throw new GetUnitException("ERROR: One or both of the units are incorrect.")
                        : enumUnit;
                }

                return Convert(value, fromEnumUnit, toEnumUnit).ToString(CultureInfo.CurrentCulture);
            }
            catch (GetUnitException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A PROGRAM ERROR OCCURRED: {ex}");
                return null;
            }
        }        
        static ImperialLengthConverter()
        {
            BaseUnit = ImperialLengthUnits.Thou;
            RegisterAbbreviation(ImperialLengthUnits.Thou, "th");
            
            RegisterConversion(ImperialLengthUnits.Inch, l => l / 1000, l => l * 1000, "in");
            RegisterConversion(ImperialLengthUnits.Foot, l => l / 12 / 1000, l => l * 1000 * 12, "ft");
            RegisterConversion(ImperialLengthUnits.Yard, l => l / 12 / 1000 / 3, l => l * 1000 * 12 * 3, "yd");
            RegisterConversion(ImperialLengthUnits.Furlong, l => l / 12 / 1000 / 3 / 220, l => l * 1000 * 12 * 3 * 220, "fur");
        }
    }
}