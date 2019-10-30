using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetBankingApp;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace AddressCheckerTest
{
    [TestClass]
    public class AddressChecker
    {
        //int fieldId values for constructor: 0 - StreetAddress, 1 - City, 2 - State/Province, 3 - ZipCode, 4 - Country
        protected string _CheckedInput = null;
        public string CheckedInput { get { return _CheckedInput; } }

        [TestMethod]
        public void AddressCheck(int fieldId, string input)
        {
            switch (fieldId)
            {
                case 0:
                    bool match = Regex.IsMatch(input, @"^\d+ [A-Za-z ]+$");
                    if (match == true)
                    {
                        Console.WriteLine("The street address {0} is valid.", input);
                        _CheckedInput = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid street address.", input);
                        break;
                    }
                case 1:
                    match = Regex.IsMatch(input, @"^[A-Za-z ]+$");
                    if (match == true)
                    {
                        Console.WriteLine("The city name {0} is valid.", input);
                        _CheckedInput = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid city name.", input);
                        break;
                    }
                case 2:
                    match = Regex.IsMatch(input, @"^[A-Za-z ]+$");
                    if (match == true)
                    {
                        Console.WriteLine("The state name {0} is valid.", input);
                        _CheckedInput = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid state name.", input);
                        break;
                    }
                case 3:
                    match = Regex.IsMatch(input, @"^\d{5}|\d{5}-\d{4}$");
                    if (match == true)
                    {
                        Console.WriteLine("The zip code {0} is valid.", input);
                        _CheckedInput = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid zip code.", input);
                        break;
                    }
                case 4:
                    match = Regex.IsMatch(input, @"^[A-Za-z ]+$");
                    if (match == true)
                    {
                        Console.WriteLine("The country name {0} is valid.", input);
                        _CheckedInput = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid country name.", input);
                        break;
                    }
                default:
                    Console.WriteLine("That is not a valid field id.");
                    break;
            }
        }


    }

    [TestClass]
    class AddressSetter
    {
        protected string _FullAddress;
        protected string _StreetAddress;
        protected string _City;
        protected string _State;
        protected string _ZipCode;
        protected string _Country;
        protected static Dictionary<int, string> AddressFields = new Dictionary<int, string>()
        {
            {0, "Please enter your street address."},
            {1, "Please enter your city name." },
            {2, "Please enter your State name." },
            {3, "Please enter your Zip code." },
            {4, "Please enter your Country name." }
        };
        public static AddressChecker checker = new AddressChecker();

        [TestMethod]
        public static void getFullAddress()
        {
            string input;
            foreach (KeyValuePair<int, string> entry in AddressFields)
            {
                Console.WriteLine(entry.Value);
                input = Console.ReadLine();
                checker.AddressCheck(entry.Key, input);
            }



        }
    }
}
