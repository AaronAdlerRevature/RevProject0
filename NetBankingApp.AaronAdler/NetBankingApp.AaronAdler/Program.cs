using System;
using System.Collections.Generic;

namespace NetBankingApp.AaronAdler
{
    class Program
    {
        static void Main()
        {
            TimeSim.initTime();
            Console.WindowWidth = 140;

            UserRegistrator.WelcomeMessage();
            UserRegistrator.getUsername();
            UserRegistrator.getPassword();
            Customer newcust = new Customer(UserRegistrator.Username, UserRegistrator.Password);

            while (!Menu.ExitCode)
            {
                Menu.MenuGreeting();
                Menu.DisplayMenu();
                Menu.getMenuInput();
                Menu.MenuRedirect(Menu.checkedInput);
                Menu.ReturnOrExit();
            }


        }
    }
}
/*
 * 
 *            AccCreator.Greeting();
           AccCreator.ParseInput();
           AccCreator.Creator(AccCreator.parsedInput);

           TimeSim.PassMonth(11);
           TimeSim.checkTermDeposit();
           TimeSim.PassMonth(1);
           TimeSim.checkTermDeposit();
 * 
 * 
 */
