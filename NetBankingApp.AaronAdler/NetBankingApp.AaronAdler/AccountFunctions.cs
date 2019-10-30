using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable CA1303 // Do not pass literals as localized parameters

namespace NetBankingApp.AaronAdler
{
    static class AccCreator
    {
        static string AccId;
        static decimal InterestRate;
        static int InstallmentTypeId;
        static decimal InitialAmount;
        static int _parsedInput;
        public static int parsedInput { get { return _parsedInput; } }

        public static void Greeting()
        {
            Console.WriteLine("Please select the type of account you wish to open.\n" +
                "0 - Checking\n1 - Business\n2 - Take out a loan\n3 - Term Deposit");
        }
        public static void ParseInput()
        {
            string input;
            bool isint;
            while (true)
            {
                input = Console.ReadLine();
                isint = int.TryParse(input, out _parsedInput);
                if (isint == false || Math.Abs(_parsedInput) > 3)
                {
                    Console.WriteLine("That is not a valid selection.");
                    Greeting();
                }
                else
                {
                    break;
                }
            }

        }
        public static void setInterest()
        {
            string input;
            bool parsed = false;
            while (true)
            {
                Console.WriteLine("Please enter a decimal value for the interest rate on this account.");
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out InterestRate);
                if (parsed && InterestRate > 0) { break; }
                else { Parsers.InvalidInput(); }
            }
        }
        public static void setInstallment()
        {
            string input;
            bool parsed = false;
            while (true)
            {

                Console.WriteLine("Please select your installment type:\n0 - Monthly\n1 - Yearly");

                input = Console.ReadLine();
                parsed = int.TryParse(input, out InstallmentTypeId);
                if (parsed && InstallmentTypeId >= 0 && InstallmentTypeId < 2) { break; } else { Console.WriteLine("That is an invalid selection"); }
            }
        }
        public static void setInitialAmount()
        {
            string input;
            bool parsed = false;
            while (true)
            {
                Console.WriteLine("Please enter the amount for your note.");
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out InitialAmount);
                if (parsed && InitialAmount > 0) { InitialAmount = Math.Round(InitialAmount, 2); break; } else { Console.WriteLine("That is not a valid entry"); }
            }
        }
        public static void Creator(int selection)
        {
            void StateCreated()
            {
                Console.WriteLine("\nAccount " + AccountStore.lastmadeAccId + " has been created!");
                AccountBalance.getBalance(AccountStore.lastmadeAccId);
            }
            switch (selection)
            {
                case 0:
                    AccountStore.addAccountid('c');
                    AccId = AccountStore.lastmadeAccId;
                    Checking newChecking = new Checking(AccId);
                    AccountStore.addAccount(AccId, newChecking);
                    BalanceTransact.Deposit();
                    AccountBalance.setBalance(AccId, BalanceTransact.checkedInput);
                    StateCreated();
                    TransactionStore.AddTransferTransaction(AccId, null, 0, 0, BalanceTransact.checkedInput);
                    break;
                case 1:
                    AccountStore.addAccountid('b');
                    AccId = AccountStore.lastmadeAccId;
                    Business newBusiness = new Business(AccId);
                    AccountStore.addAccount(AccId, newBusiness);
                    BalanceTransact.Deposit();
                    AccountBalance.setBalance(AccId, BalanceTransact.checkedInput);
                    StateCreated();
                    TransactionStore.AddTransferTransaction(AccId, null, 1, 0, BalanceTransact.checkedInput);
                    break;
                case 2:
                    AccountStore.addAccountid('l');
                    AccId = AccountStore.lastmadeAccId;
                    AccCreator.setInterest();
                    AccCreator.setInstallment();
                    AccCreator.setInitialAmount();
                    Loan newLoan = new Loan(AccId, AccCreator.InterestRate, AccCreator.InstallmentTypeId, AccCreator.InitialAmount);
                    AccountStore.addAccount(AccId, newLoan);
                    AccountBalance.setBalance(AccId, AccCreator.InitialAmount);
                    StateCreated();
                    TransactionStore.AddTransferTransaction(AccId, null, 2, 0, AccCreator.InitialAmount);
                    break;
                case 3:
                    AccountStore.addAccountid('t');
                    AccId = AccountStore.lastmadeAccId;
                    AccCreator.setInterest();
                    AccCreator.setInitialAmount();
                    TermDeposit newCD = new TermDeposit(AccId, InterestRate);
                    AccountStore.addAccount(AccId, newCD);
                    AccountBalance.setBalance(AccId, AccCreator.InitialAmount);
                    StateCreated();
                    TransactionStore.AddTransferTransaction(AccId, null, 3, 0, AccCreator.InitialAmount);
                    break;

                default:
                    Console.WriteLine("That is not a valid selection");
                    return;
            }
        }
        public static void OverCreator(string AssociatedAccountId, decimal ammount)
        {
            AccountStore.addAccountid('o');
            AccId = AccountStore.lastmadeAccId;
            InterestRate = AccountStore.StoredAccounts[AssociatedAccountId].InterestRate;
            OverdraftFaucility newOverdraft = new OverdraftFaucility(AccId, AssociatedAccountId, ammount);
            AccountStore.addAccount(AccId, newOverdraft);
            AccountStore.StoredAccounts[AssociatedAccountId]._AssociatedAccountId = AccId;
            TransactionStore.AddTransferTransaction(AccId, AssociatedAccountId, 4, 0, ammount);

        }
        public static void AccountCloser()
        {
            AccViewer.getAccounts();
            while (true)
            {
                Console.WriteLine("Please enter the account Id you wish to close...");
                AccId = Console.ReadLine();
                if (AccountStore.StoredAccounts.ContainsKey(AccId)) { break; }
                else { Parsers.InvalidInput(); }
            }
            if (AccountBalance.Balance[AccId] != 0 || !(AccountStore.StoredAccounts[AccId]._AssociatedAccountId is null))
            {
                Console.WriteLine("You cannot close an account with an outstanding balance!\nAccount " + AccId + " has a balance of $" +
                    AccountBalance.Balance[AccId]);
                if (!(AccountStore.StoredAccounts[AccId]._AssociatedAccountId is null))
                {
                    Console.WriteLine("and the associated account " + AccountStore.StoredAccounts[AccId]._AssociatedAccountId + " has a balance of $" +
                        AccountBalance.Balance[AccountStore.StoredAccounts[AccId]._AssociatedAccountId]);
                }

            }
            else
            {
                TransactionStore.AddCloseTransaction(AccId);
                AccountStore.StoredAccounts.Remove(AccId);
                AccountBalance.Balance.Remove(AccId);
                Console.WriteLine("Account " + AccId + " has been closed!");

            }
        }
        public static void AccountCloser(string accId)
        {
            TransactionStore.AddCloseTransaction(accId);
            AccountStore.StoredAccounts.Remove(accId);
            AccountBalance.Balance.Remove(accId);
            Console.WriteLine("Account " + accId + " has been closed!");
        }
    }

}