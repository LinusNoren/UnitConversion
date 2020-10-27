using System;

namespace ImperialUnitConversion
{
    static class Program
    {
        static void Main()
        {
            const string endCommand = "EXIT";
            
            Console.WriteLine("Convert from one Imperial unit to another.");
            Console.WriteLine("Valid units are:");

            foreach (var unit in Enum.GetNames(typeof(ImperialLengthUnits)))
                Console.WriteLine(unit);

            Console.WriteLine("Format the conversion like this: [number] [FromUnit] in [ToUnit]");
            Console.WriteLine($"Write {endCommand} to terminate the program.");
            
            while (true)
            {
                Console.Write("Convert: ");

                var input = Console.ReadLine();

                if (input?.ToUpper() == endCommand)
                    break;

                var result = ImperialLengthConverter.Convert(input);

                if (result != null)
                    Console.WriteLine($"Answer: {result}");
            }
        }
    }
}
