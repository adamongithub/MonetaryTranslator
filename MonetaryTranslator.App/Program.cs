﻿using System;
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
            var whole = String.Empty;
            var remainder = String.Empty;
            string pence = ProcessTens(remainder, 0, 1);

            try
            {
                ValidateInput(input);
                input = input.Replace(_Culture.NumberFormat.CurrencySymbol, "");
                whole = GetWholeAndRemainder(input, ref remainder);
                

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

        public static string TranslateFromArray(char index, string[] array, string prefix, string suffix)
        {
            var t = array[int.Parse(index.ToString())];
            return String.IsNullOrEmpty(t) ? "" : prefix + t + suffix;
        }
        public static string ProcessTens(string str, int firstIndex = 0, int secondIndex = 1)
        {
            string output = "";
            if (str[firstIndex] == '1')
            {
                output += TranslateFromArray(str[secondIndex], _Teens, " and ", "");
            }
            else
            {
                output += TranslateFromArray(str[firstIndex], _Tens, " and ", "");
                output += TranslateFromArray(str[secondIndex], _Units, "", "");
            }

            return output;
        }
    }
}
