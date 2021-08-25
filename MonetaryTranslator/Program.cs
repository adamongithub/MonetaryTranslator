using System;
using System.Globalization;

namespace MonetaryTranslator
{
    class Program
    {
        private static CultureInfo _Culture = CultureInfo.CreateSpecificCulture("en-GB");

        static void Main(string[] args)
        {
            
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
