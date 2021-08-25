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

        public static void Main(string[] args)
        {
            Console.WriteLine("Please Enter your value");
            var input = Console.ReadLine();

            try
            {
                ValidateInput(input);
            }
            catch (Exception e)
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
