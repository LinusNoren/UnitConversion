using System;

namespace ImperialUnitConversion
{
    public class GetUnitException : Exception
    {
        public GetUnitException(string message)
            : base(message)
        {
        }
    }
}
