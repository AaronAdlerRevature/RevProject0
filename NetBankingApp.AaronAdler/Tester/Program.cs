using System;
using System.Text.RegularExpressions;

namespace Tester
{


    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter fieldId.\n0 - StreetAddress, 1 - City, 2 - State/Province, 3 - ZipCode, 4 - Country");
                int fieldId = int.Parse(Console.ReadLine());
                Console.WriteLine("Please a string to test:");
                string Input = Console.ReadLine();
                AddressChecker test = new AddressChecker(fieldId, Input);
                Console.WriteLine("Test again? (y/n)");
                string testagain = Console.ReadLine();
                if (testagain.Equals("y") == false)
                {
                    Console.WriteLine(testagain.Equals("y"));
                    break; 
                }
            }
         
        }

        public class AddressChecker
        {
            //int fieldId values for constructor: 0 - StreetAddress, 1 - City, 2 - State/Province, 3 - ZipCode, 4 - Country
            protected string _CheckedInput = null;
            public AddressChecker(int fieldId, string input)
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
    }
}
