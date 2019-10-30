using System;
using System.Collections.Generic;

namespace NetBankingApp.AaronAdler
{
    static class TimeSim
    {
        static DateTime _StartTime = DateTime.Now;
        public static DateTime StartTime { get { return _StartTime; } }
        static DateTime _CurrentTime = DateTime.Now;
        public static DateTime CurrentTime { get { return _CurrentTime; } }
        static Dictionary<string, TermDeposit> TermDeposits = new Dictionary<string, TermDeposit>();

        public static void initTime()
        {
            
        }

        public static void PassTime()
        {
            int parsedInt;
            Console.WriteLine("How many months would you like ~~TrAvEl InTo ThE fUtUrE?~~");
            string input = Console.ReadLine();
            Parsers.IntCheck(input, out parsedInt);
            Console.WriteLine("We dont need turn signals where we're going!");
            System.Threading.Thread.Sleep(1500);
            PassMonth(parsedInt);
        }

        public static void PassMonth(int n)
        {
            for(int i = 0; i < n; i++)
            {
                _CurrentTime = _CurrentTime.AddMonths(1);
                Console.WriteLine(CurrentTime);
                simInterest();
                grabTerms();
                checkTermDeposit();
            }
        }

        public static void simInterest()
        {
            int countMonth = 0;
            countMonth++;
            foreach (KeyValuePair<string, Account> entry in AccountStore.StoredAccounts)
            {
                decimal interest = decimal.Round(AccountBalance.Balance[entry.Key] * AccountStore.StoredAccounts[entry.Key].InterestRate, 2);
                if (entry.Value.InstallmentTypeId == 0)
                {
                    AccountBalance.Balance[entry.Key] += interest;
                    TransactionStore.AddTransferTransaction(entry.Key, null, AccountStore.AccCharToId[entry.Key[0]], 6, interest);
                }
                else if(entry.Value.InstallmentTypeId == 1 && countMonth%12 == 0)
                {
                    AccountBalance.Balance[entry.Key] += interest;
                    TransactionStore.AddTransferTransaction(entry.Key, null, AccountStore.AccCharToId[entry.Key[0]], 6, interest);
                }
            }
        }
        public static void grabTerms()
        {
            TermDeposits.Clear();
            foreach(KeyValuePair<string,Account> entry in AccountStore.StoredAccounts)
            {
                if(entry.Value.AccountId[0] == 't')
                {
                    TermDeposits.Add(entry.Key, (TermDeposit)entry.Value);
                }
            }
        }

        public static void checkTermDeposit()
        {
            int maturity;
            foreach(KeyValuePair<string, TermDeposit> entry in TermDeposits)
            {
                maturity = (int)_CurrentTime.Subtract(entry.Value.CreationTime).TotalDays;
                if(maturity > 365)
                {
                    TermMature(entry.Key, AccountBalance.Balance[entry.Key]);
                }
            }
        }

        public static void TermMature(string TermaccId, decimal ammount)
        {
            AccountStore.addAccountid('c');
            string AccId = AccountStore.lastmadeAccId;
            Checking newChecking = new Checking(AccId);
            AccountStore.addAccount(AccId, newChecking);
            AccountBalance.setBalance(AccId, ammount);
            AccountBalance.setBalance(TermaccId, 0);
            TransactionStore.AddTransferTransaction(AccId, null, 0, 0, ammount);
            TransactionStore.AddTransferTransaction(TermaccId, null, 3, 5, ammount);
            AccCreator.AccountCloser(TermaccId);
        }
    }
}
