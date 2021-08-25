using System;
using System.Globalization;

namespace MonetaryTranslator
{
    class Program
    {
        private static CultureInfo _Culture = CultureInfo.CreateSpecificCulture("en-GB");

        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter your value");
            var input = Console.ReadLine();

            try
            {
                InputIsValid(input);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Input is Invald for the following reason:");
                Console.WriteLine(e.Message);
                return;
            }
        }

        private static bool InputIsValid(string input)
        {
            if (decimal.TryParse(input, NumberStyles.Currency, _Culture, out decimal d)
                && d >= 0
                && d <= 999999999)
            {
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Number cannot be negative and must be smaller or equal to 10^9.");
            }
        }
    }
}
