using System.Collections.Generic;
using System;




namespace NetBankingApp.AaronAdler
{
    static class AccountStore
    {
        static string _lastmadeAccId;
        public static string lastmadeAccId { get { return _lastmadeAccId; } }
        public static Dictionary<char, List<int>> AccountIds = new Dictionary<char, List<int>>()
        {
            {'c', new List<int>() },
            {'b', new List<int>() },
            {'l', new List<int>() },
            {'t', new List<int>() },
            {'o', new List<int>() }
        };
        readonly public static Dictionary<char, string> AccCharToName = new Dictionary<char, string>()
        {
            {'c', "Checking" },
            {'b', "Business" },
            {'t', "Term Deposit" },
            {'l', "Loan" },
            {'o', "Overdraft Faucility" }
        };
        readonly public static Dictionary<char, int> AccCharToId = new Dictionary<char, int>()
        {
            {'c', 0 },
            {'b', 1 },
            {'t', 3 },
            {'l', 2 },
            {'o', 4 }
        };
        public static Dictionary<string, Account> StoredAccounts = new Dictionary<string, Account>();
        public static void addAccountid(char accType)
        {
            int accNumb;           
            accNumb = AccountStore.AccountIds[accType].Count;
            AccountStore.AccountIds[accType].Add(accNumb);
            AccountStore._lastmadeAccId = accType + accNumb.ToString();            
        }
        public static void addAccount(string accId, Account account)
        {
            StoredAccounts.Add(accId, account);
        }
        public static void AccountTypeLister()
        {
            foreach (KeyValuePair<char, List<int>> entry in AccountIds)
            {
                Console.WriteLine(AccCharToName[entry.Key]);
                foreach (int account in entry.Value)
                {
                    Console.WriteLine(account);
                }
            }
        }

    }

    static class AccViewer
    {
        static string AccId;
        static List<string> AllAccCol = new List<string>
        {
            "AccountId",
            "AccountType",
            "Balance",
            "InterestRate",
            "CreationTime",
            "CanOverdraft",
            "Transferable"
        };

        public static void getAccounts()
        {
            if (AccountStore.StoredAccounts.Count == 0)
            {
                Console.WriteLine("You have no opened accounts!");
            }
            else
            {
                Console.WriteLine("|{0,-10}|{1,-20}|{2,-15}|{3,-15}|{4,-25}|{5,-15}|{6,-15}|",
                    AllAccCol[0], AllAccCol[1], AllAccCol[2], AllAccCol[3], AllAccCol[4], AllAccCol[5], AllAccCol[6]);
                Console.WriteLine("|" + new string('-', 10) + "|" + new string('-', 20) + "|" + new string('-', 15) + "|" + new string('-', 15) + "|"
                    + new string('-', 25) + "|" + new string('-', 15) + "|" + new string('-', 15) + "|");
                foreach (KeyValuePair<string, Account> entry in AccountStore.StoredAccounts)
                {
                    Console.WriteLine("|{0,-10}|{1,-20}|{2,-15}|{3,-15}|{4,-25}|{5,-15}|{6,-15}|", entry.Key, AccountStore.AccCharToName[entry.Key[0]], AccountBalance.Balance[entry.Key],
                        entry.Value.InterestRate, entry.Value.CreationTime, entry.Value.CanOverdraft, entry.Value.CanTransfer);
                }
            }
        }


    }
}

