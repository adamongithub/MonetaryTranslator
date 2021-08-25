using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
                Console.WriteLine(ProcessInput(input));
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Input is Invalid for the following reason:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
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

        public static string GetWholeAndRemainder(string input, ref string remainder)
        {
            string whole;
            if (input.Contains('.'))
            {
                var wholeremainder = input.Split('.');
                whole = wholeremainder[0];
                remainder = wholeremainder[1];
            }
            else
            {
                whole = input;
            }

            return whole;
        }

        public static string TranslateGroup(string group)
        {
            var hundreds = char.GetNumericValue(group[0]) > 0;
            var tens = char.GetNumericValue(group[1]) > 0;
            var units = char.GetNumericValue(group[2]) > 0;
            var combined = false;

            string output = "";
            
            if (hundreds)
            {
                output += _Units[int.Parse(group[0].ToString())];
                output += " hundred";
            }

            if (tens)
            {
                output += hundreds ? " and " : "";

                if (group[1] == '1')
                {
                    combined = true;
                    output += _Teens[int.Parse(group[1].ToString())];
                }
                else
                {
                    output += _Tens[int.Parse(group[1].ToString())];
                }
            }

            if (units && !combined)
            {
                output += _Units[int.Parse(group[2].ToString())];
            }

            return output;
        }

        public static string TranslateAndAddDenomination(string group, string denomination, bool first = false)
        {
            string output = TranslateGroup(group);
            output += " " + (String.IsNullOrEmpty(output) && !first ? "" : denomination);
            return output;
        }

        public static string ProcessInput(string input)
        {
            var remainder = String.Empty;
            var whole = GetWholeAndRemainder(input, ref remainder);           
            whole = whole.Replace(_Culture.NumberFormat.CurrencySymbol, "");

            var groups = whole.Split(',');
            var strings = new List<String>();
            if (!String.IsNullOrEmpty(remainder))
            {
                string pence = TranslateGroup(remainder.PadLeft(3, '0'));
                strings.Add(String.IsNullOrEmpty(pence) ? "" : "and " + pence + " pence");
            }

            var i = 0;
            for (var j = groups.Length - 1; j >= 0; j--)
            {
                if (groups[j] != null)
                {
                    strings.Add(TranslateAndAddDenomination(groups[j].PadLeft(3, '0'), _Denominations[i], i == 0));
                    i++;
                }
            }

            strings.Reverse();
            return String.Join(" ", strings);
        }
    }
}
