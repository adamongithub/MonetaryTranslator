using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonetaryTranslator.App
{
    public static class Program
    {
        private static CultureInfo _Culture = CultureInfo.CreateSpecificCulture("en-GB");
        private static string[] _Units = new[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        private static string[] _Teens = new[] {
            "",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        private static string[] _Tens = new[] {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        private static string[] _Denominations = new string[]
        {
            "pounds",
            "thousand",
            "million"
        };

        public static void Main(string[] args)
        {
            Console.WriteLine("Please Enter your value");
            var input = Console.ReadLine();

            try
            {
                ValidateInput(input);
                input = input.Replace(_Culture.NumberFormat.CurrencySymbol, "");

            }
            catch (Exception e)
            {
                Console.WriteLine("Input is Invald for the following reason:");
                Console.WriteLine(e.Message);
                return;
            }
        }

        /// <summary>
        /// Known issues:
        /// 1) Allows decimal places greater than 2.
        /// </summary>
        public static bool ValidateInput(string input)
        {
            if (decimal.TryParse(input, NumberStyles.Currency, _Culture, out decimal d))
            { 
                if (d >= 0 && d <= 999999999)
                {
                    return true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Number cannot be negative and must be smaller or equal to 10^9.");
                }
            }
            else
            {
                throw new ArgumentException("Input is not in the correct format.");

            }
        }
    }
}
