using System;
using System.Collections.Generic;

namespace ImperialUnitConversion
{
    /// <summary>
    /// Generic class for converting between values of different units.
    /// </summary>
    /// <typeparam name="TUnitType">The type representing the unit type (eg. enum)</typeparam>
    /// <typeparam name="TValueType">The type of value for this unit (float, decimal, int, etc.)</typeparam>
    abstract class UnitConverter<TUnitType, TValueType>
    {
        /// <summary>
        /// The base unit, which all calculations will be expressed in terms of.
        /// </summary>
        protected static TUnitType BaseUnit;

        /// <summary>
        /// Dictionary of functions to convert from the base unit type into a specific type.
        /// </summary>
        static readonly Dictionary<TUnitType, Func<TValueType, TValueType>> ConversionsTo =
            new Dictionary<TUnitType, Func<TValueType, TValueType>>();

        /// <summary>
        /// Dictionary of functions to convert from the specified type into the base unit type.
        /// </summary>
        static readonly Dictionary<TUnitType, Func<TValueType, TValueType>> ConversionsFrom =
            new Dictionary<TUnitType, Func<TValueType, TValueType>>();

        /// <summary>
        /// Converts a value from one unit type to another.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="from">The unit type the provided value is in.</param>
        /// <param name="to">The unit type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        protected static TValueType Convert(TValueType value, TUnitType from, TUnitType to)
        {
            if (from.Equals(to))
                return value;

            TValueType valueInBaseUnit;

            if (from.Equals(BaseUnit))
                valueInBaseUnit = value;
            else
            {
               if(ConversionsFrom.TryGetValue(from, out var func ))
                    valueInBaseUnit = func(value);
               else
                   throw new KeyNotFoundException($"The conversion functions for the unit {from} has not been registered.");
            }

            TValueType valueInRequiredUnit;

            if (to.Equals(BaseUnit))
                valueInRequiredUnit = valueInBaseUnit;
            else
            {
                if (ConversionsTo.TryGetValue(to, out var func))
                    valueInRequiredUnit = func(valueInBaseUnit);
                else
                    throw new KeyNotFoundException($"The conversion functions for the unit {to} has not been registered.");
            }

            return valueInRequiredUnit;
        }

        /// <summary>
        /// Registers functions for converting to/from a unit.
        /// </summary>
        /// <param name="convertToUnit">The type of unit to convert to/from, from the base unit.</param>
        /// <param name="conversionTo">A function to convert from the base unit.</param>
        /// <param name="conversionFrom">A function to convert to the base unit.</param>
        /// <param name="abbreviation">A abbreviation to the unit</param>
        protected static void RegisterConversion(TUnitType convertToUnit, Func<TValueType, TValueType> conversionTo,
            Func<TValueType, TValueType> conversionFrom, string abbreviation = null)
        {
            if (!ConversionsTo.TryAdd(convertToUnit, conversionTo))
                throw new ArgumentException("Already exists", nameof(convertToUnit));

            if (!ConversionsFrom.TryAdd(convertToUnit, conversionFrom))
                throw new ArgumentException("Already exists", nameof(convertToUnit));

            if (abbreviation != null)
                RegisterAbbreviation(convertToUnit, abbreviation);

        }

        /// <summary>
        /// Dictionary of Abbreviations.
        /// </summary>
        private static readonly Dictionary<string, TUnitType> Abbreviation = new Dictionary<string, TUnitType>();

        /// <summary>
        ///  Registers abbreviation for a unit.
        /// </summary>
        /// <param name="unit">A unit</param>
        /// <param name="abbreviation">A abbreviation for the unit</param>
        protected static void RegisterAbbreviation(TUnitType unit, string abbreviation)
        {
            if (!Abbreviation.TryAdd(abbreviation, unit))
                throw new ArgumentException("Already exists", nameof(unit));
        }
        /// <summary>
        /// Try to get the unit for an abbreviation.
        /// </summary>
        /// <param name="abbreviation">A abbreviation for a unit</param>
        /// <param name="unitType">The unit for the abbreviation</param>
        /// <returns>False if the abbreviation don't exist</returns>
        protected static bool TryGetUnit(string abbreviation, out TUnitType unitType) => Abbreviation.TryGetValue(abbreviation, out unitType);
    }
}