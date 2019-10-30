using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    public static class getTypeName
    {
        public static void getAccountType(int typeId)
        {
            switch (typeId)
            {
                case 0:
                    Console.WriteLine("c - Checking.");
                    break;
                case 1:
                    Console.WriteLine("b - Business");
                    break;
                case 2:
                    Console.WriteLine("l - Loan");
                    break;
                case 3:
                    Console.WriteLine("t - Term Deposit (Certificate of Deposit).");
                    break;
                case 4:
                    Console.WriteLine("o - Overdraft Facility.");
                    break;
                default:
                    Console.WriteLine("An invalid account type id has been passed.");
                    break;
            }
        }
        public static void getInstallmentType(int InstallmentType)
        {
            switch (InstallmentType)
            {
                case 0:
                    Console.WriteLine("Monthly");
                    break;
                case 1:
                    Console.WriteLine("Yearly");
                    break;
                default:
                    Console.WriteLine("An invalid installment type id has been passed.");
                    break;
            }

        }
    }
}
