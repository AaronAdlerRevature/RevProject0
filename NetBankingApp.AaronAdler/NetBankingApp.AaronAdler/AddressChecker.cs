using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetBankingApp.AaronAdler
{
    public class AddressChecker
    {
        //int fieldId values for constructor: 0 - StreetAddress, 1 - City, 2 - State/Province, 3 - ZipCode, 4 - Country
        protected string _CheckedInput = null;
        protected bool _FailedCheck;
        public string CheckedInput { get { return _CheckedInput; }  }
        public bool FailedCheck {  get { return _FailedCheck; } }
        public void AddressCheck(int fieldId, string input)
        {
            _FailedCheck = false;
            switch (fieldId)
            {
                case 1:
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
                        _FailedCheck = true;
                        break;
                    }
                case 2:
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
                        _FailedCheck = true;
                        break;
                    }
                case 3:
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
                        _FailedCheck = true;
                        break;
                    }
                case 4:
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
                        _FailedCheck = true;
                        break;
                    }
                case 5:
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
                        _FailedCheck = true;
                        break;
                    }
                default:
                    Console.WriteLine("That is not a valid field id.");
                    break;
            }
        }
    }

    static class AddressSetter
    {
        static string output;
        private static Dictionary<int, string> AddressFieldChecks = new Dictionary<int, string>()
        {
            {1, "Street Address: "},
            {2, "City name: " },
            {3, "State name: " },
            {4, "Zip Code: " },
            {5, "Country name: " }
        };
        public static Dictionary<int, string> _AddressFieldStore = new Dictionary<int, string>()
        {
            {0, ""/*this is where full address will be concatinated.*/ }
        };

        public static AddressChecker checker = new AddressChecker();

        public static void getFullAddress()
        {

            foreach (KeyValuePair<int, string> entry in AddressFieldChecks)
            {
                bool PassedCheck = false;
                string input;
                while (PassedCheck == false)
                {
                    Console.WriteLine("Please enter your " + entry.Value);
                    input = Console.ReadLine();
                    checker.AddressCheck(entry.Key, input);
                    PassedCheck = !checker.FailedCheck;
                    output = input;
                }
                _AddressFieldStore.Add(entry.Key, output);
                _AddressFieldStore[0] += output + ", ";
            }
            _AddressFieldStore[0] = _AddressFieldStore[0].Remove(_AddressFieldStore[0].Length - 2);
        }
        public static void displayFullAddress()
        {
            foreach (KeyValuePair<int, string> store in AddressFieldChecks)
            {
                Console.WriteLine("The {0} is: {1}", AddressFieldChecks[store.Key], _AddressFieldStore[store.Key]);
            }
            Console.WriteLine("The Full Address is: {0}", _AddressFieldStore[0]);
        }

        
    }
}
