using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    static class Parsers
    {
        static char _parsedYN;
        public readonly static Dictionary<char, bool> YNtoBool = new Dictionary<char, bool>()        
        {
            {'y' , true },
            {'n' , false }
        };
        public readonly static Dictionary<int, bool> BintoBool = new Dictionary<int, bool>()
        {
            {0, false },
            {1, true }
        };
        public readonly static List<string> YN = new List<string> { "y", "n" };

        static List<int> isBin = new List<int>() { 0, 1 };
        static int _parsedBin;
        public static int _parsedInt;

        public static char parsedYN { get { return _parsedYN; } }
        public static int parsedBin { get { return _parsedBin; } }

        public static void InvalidInput()
        {
            Console.WriteLine("That is not a valid input!");
        }
        public static void YNCheck()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine();
                input = input.ToLower();
                if (YN.Contains(input)) { _parsedYN = input[0]; break; } 
                else { InvalidInput(); Console.WriteLine("Please enter (y) or (n)."); }
            }
        }
        public static void BinCheck(string input, out string _input)
        {
            while (true)
            {
                bool parsed = int.TryParse(input, out _parsedInt);
                if (parsed && isBin.Contains(_parsedInt)) { _parsedBin = _parsedInt; break; } else { InvalidInput(); input = Console.ReadLine(); }
            }
            _input = input;
        }

        public static void IntCheck(string input, out int _parsedInt)
        {
            bool parsed;
            while (parsed = false)
            {
                int parsedInt;
                parsed = int.TryParse(input, out parsedInt);
                if (parsed = false) { InvalidInput(); input = Console.ReadLine(); }
                else { _parsedInt = parsedInt;  break; }
            }
            _parsedInt = Convert.ToInt32(input);
            
        }
    }
}
