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
                ValdiateInput(input);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Input is Invald for the following reason:");
                Console.WriteLine(e.Message);
                return;
            }

            input = input.Replace(_Culture.NumberFormat.CurrencySymbol, "");


        }

        /// <summary>
        /// Known issues:
        /// 1) Allows decimal places greater than 2.
        /// </summary>
        private static bool ValdiateInput(string input)
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
