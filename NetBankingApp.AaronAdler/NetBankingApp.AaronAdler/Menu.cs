using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    static class Menu
    {
        static bool _ExitCode;
        public static bool ExitCode { get { return _ExitCode; } }
        static string input;
        static int _checkedInput;
        public static int checkedInput { get { return _checkedInput; } }
        static Dictionary<int, string> MenuItems = new Dictionary<int, string>()
        {
            {0, "Open new account." },
            {1, "Close an account." },
            {2, "Withdraw from an account." },
            {3, "Make a deposit." },
            {4, "Transfer between accounts" },
            {5, "Make a payment." },
            {6, "Display accounts." },
            {7, "Display transactions for all account." },
            {8, "Simulate the passage of time." }
        };
        public static void MenuGreeting()
        {
            Console.WriteLine("Welcome to the main menu. Please select from the options below...\n");
        }
        public static void DisplayMenu()
        {
            foreach (KeyValuePair<int, string> Menu in MenuItems)
            {
                Console.WriteLine(Menu.Key.ToString() + " - " + Menu.Value);
            }
        }
        public static void getMenuInput()
        {
            while (true) 
            {
                input = Console.ReadLine();
                bool isint = int.TryParse(input, out _checkedInput);
                if (isint == false || MenuItems.ContainsKey(_checkedInput) == false)
                {
                    Console.WriteLine("That is not a valid selection.");
                    DisplayMenu();
                }
                else
                {
                    break;
                }
            }
            
        }
        public static void MenuRedirect(int input)
        {
            switch (input)
            {
                case 0:
                    AccCreator.Greeting();
                    AccCreator.ParseInput();
                    AccCreator.Creator(AccCreator.parsedInput);
                    break;
                case 1:
                    AccCreator.AccountCloser();
                    break;
                case 2:
                    BalanceTransact.SelectAcc('w');
                    break;
                case 3:
                    BalanceTransact.SelectAcc('d');
                    break;
                case 4:
                    BalanceTransact.SelectAcc('t');
                    break;
                case 5:
                    BalanceTransact.Pay();
                    break;
                case 6:
                    AccViewer.getAccounts();
                    break;
                case 7:
                    TransactionViewer.Display();
                    break;
                case 8:
                    TimeSim.PassTime();
                    break;
            }
        }

        public static void ReturnOrExit()
        {
            string _input;
            Console.WriteLine("Would you like to return to the menu, or exit the application?\n0 - Return\n1 - Exit");

            input = Console.ReadLine();
            Parsers.BinCheck(input, out _input);
            Menu._ExitCode = Parsers.BintoBool[Convert.ToInt32(_input)];
        }
    }
}
