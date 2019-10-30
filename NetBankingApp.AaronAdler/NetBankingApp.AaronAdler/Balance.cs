using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    static class AccountBalance
    {
        static Dictionary<string, decimal> _Balance = new Dictionary<string, decimal>();
        public static Dictionary<string, decimal> Balance { get { return _Balance; } }
        public static void getBalance(string AccId)
        {
            Console.WriteLine("The current balance for account " + AccId + " is: $" + _Balance[AccId]);
        }

        public static void setBalance(string accId, decimal balance)
        {
            _Balance[accId] = balance;
        }
    }

    static class BalanceTransact
    {
        static decimal _checkedInput;
        static bool parsed = false;
        static string input;
        public static decimal checkedInput { get { return _checkedInput; } }


        public static void SelectAcc(char TransferType)
        {
            int existCheck = 0;
            Dictionary<string, object> canTransfer = new Dictionary<string, object>();
            foreach (KeyValuePair<string, Account> entry in AccountStore.StoredAccounts)
            {
                if (entry.Value.CanTransfer)
                {
                    canTransfer.Add(entry.Key,entry.Value);
                }
            }
            if(canTransfer.Count == 0) { TransferGreeting(TransferType + "0"); }
            else
            {
                TransferGreeting(TransferType + "1");
                while (true)
                {
                    Console.WriteLine("|{0,-15}|{1,-20}|", "AccountId", "Balance");
                    Console.WriteLine("|" + new string('-', 15) + '|' + new string('-', 20) + "|");
                    foreach (KeyValuePair<string, object> entry in canTransfer)
                    {
                        Console.WriteLine("|{0,-15}|{1,-20}|", entry.Key, AccountBalance.Balance[entry.Key]);
                        if(AccountStore.StoredAccounts[entry.Key]._AssociatedAccountId != null)
                        {

                            Console.WriteLine("*This account has an overdraft of $" + AccountBalance.Balance[AccountStore.StoredAccounts[entry.Key]._AssociatedAccountId] + 
                                ". Any deposits will go towards paying this debt.");
                        }
                    }
                    input = Console.ReadLine();
                    if (canTransfer.ContainsKey(input)) { TransferCallType(TransferType, input); break; }
                    else { Parsers.InvalidInput(); }
                }
            }    
        }
        public static void SelectAccT(char TransferType, string AccId)
        {
            int existCheck = 0;
            Dictionary<string, object> canTransfer = new Dictionary<string, object>();
            foreach (KeyValuePair<string, Account> entry in AccountStore.StoredAccounts)
            {
                if (entry.Value.CanTransfer)
                {
                    canTransfer.Add(entry.Key, entry.Value);
                }
                canTransfer.Remove(AccId);
            }
            if (canTransfer.Count == 0) { TransferGreeting(TransferType + "0"); }
            else
            {
                TransferGreeting(TransferType + "2");
                while (true)
                {
                    Console.WriteLine("|{0,-15}|{1,-20}|", "AccountId", "Balance");
                    Console.WriteLine("|" + new string('-', 15) + '|' + new string('-', 20) + "|");
                    foreach (KeyValuePair<string, object> entry in canTransfer)
                    {
                        Console.WriteLine("|{0,-15}|{1,-20}|", entry.Key, AccountBalance.Balance[entry.Key]);
                        if (AccountStore.StoredAccounts[entry.Key]._AssociatedAccountId != null)
                        {

                            Console.WriteLine("*This account has an overdraft of $" + AccountBalance.Balance[AccountStore.StoredAccounts[entry.Key]._AssociatedAccountId] +
                                ". Any deposits will go towards paying this debt.");
                        }
                    }
                    input = Console.ReadLine();
                    if (canTransfer.ContainsKey(input)) { Transfer(AccId, input); break; }
                    else { Parsers.InvalidInput(); }
                }
            }
        }
        public static void TransferGreeting(string t)
        {
            switch (t)
            {
                case "d0":
                    Console.WriteLine("You have no accounts you may deposit into!");
                    break;
                case "d1":
                    Console.WriteLine("To which account would you like to make a deposit?");
                    break;
                case "w0":
                    Console.WriteLine("You have no accounts you may withdraw from!");
                    break;
                case "w1":
                    Console.WriteLine("To which account would you like to withdraw from?");
                    break;
                case "t0":
                    Console.WriteLine("You have to accounts eligable for a transfer!");
                    break;
                case "t1":
                    Console.WriteLine("To which account would you like to transfer from?");
                    break;
                case "t2":
                    Console.WriteLine("To which account would you like to transfer to?");
                    break;
            }  
        }

        public static void TransferCallType(char t, string accId)
        {
            switch (t)
            {
                case 'd':
                    Deposit(accId);
                    break;
                case 'w':
                    Withdraw(accId);
                    break;
                case 't':
                    SelectAccT(t, accId);
                    break;
            }
        }
        public static void Deposit()
        {
            Console.WriteLine("Note: enter a decimal number with no symbles. " +
                "Any decimal past the 2nd decimal place will be rounded up.");
            while (true)
            {
                Console.WriteLine("How much would you like to deposit?");
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out _checkedInput);
                if (parsed && _checkedInput >= 0)
                {
                    _checkedInput = Math.Round(_checkedInput, 2);
                    AccountBalance.Balance[AccountStore.lastmadeAccId] = _checkedInput;
                    break;
                }
                else { Parsers.InvalidInput(); }
            }
        }
        public static void Deposit(string accId)
        {
            Console.WriteLine("Note: enter a decimal number with no symbles. " +
                "Any decimal past the 2nd decimal place will be rounded up.");
            while (true)
            {
                bool exit = false;
                Console.WriteLine("How much would you like to deposit?\nCurrent balance is: $" + AccountBalance.Balance[accId]);
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out _checkedInput);
                if (parsed && _checkedInput >= 0)
                {
                    _checkedInput = Math.Round(_checkedInput, 2);
                    OverdraftChecker(accId, _checkedInput, out _checkedInput, out bool didClose);
                    if(didClose == true)
                    {
                        AccountStore.StoredAccounts[accId]._AssociatedAccountId = null;
                    }             
                    AccountBalance.Balance[accId] += _checkedInput;
                    TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 1, _checkedInput);
                    Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId] +
                        "\nWould you like to deposit more? (y/n)");
                    Parsers.YNCheck();
                    exit = Parsers.YNtoBool[Parsers.parsedYN];
                    if(!exit == true) { break; }
                }
                else { Parsers.InvalidInput(); }
            }
        }
        public static void Deposit(string accId, decimal ammount)
        {
            {
                TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 1, ammount);
                AccountBalance.Balance[accId] += ammount;
            }
        }
        public static void Pay()
        {
            int existCheck = 0;
            Dictionary<string, object> canPay = new Dictionary<string, object>();
            foreach (KeyValuePair<string, Account> entry in AccountStore.StoredAccounts)
            {
                if (entry.Value.AccountId[0] == 'l' || entry.Value.AccountId[0] == 'o')
                {
                    canPay.Add(entry.Key, entry.Value);
                }
            }
            if (canPay.Count == 0) { Console.WriteLine("You have no accounts to pay"); }
            else
            {
                Console.WriteLine("Which account would you like to pay?");
                while (true)
                {
                    Console.WriteLine("|{0,-15}|{1,-20}|", "AccountId", "Balance");
                    Console.WriteLine("|" + new string('-', 15) + '|' + new string('-', 20) + "|");
                    foreach (KeyValuePair<string, object> entry in canPay)
                    {
                        Console.WriteLine("|{0,-15}|{1,-20}|", entry.Key, AccountBalance.Balance[entry.Key]);
                    }
                    string accId = Console.ReadLine();
                    if (canPay.ContainsKey(accId)) 
                    {
                        Console.WriteLine("How much would you like to pay?");
                        input = Console.ReadLine();
                        parsed = decimal.TryParse(input, out _checkedInput);
                        if (parsed && _checkedInput >= 0)
                        {
                            bool didClose;
                            bool exit;
                            decimal output;
                            _checkedInput = Math.Round(_checkedInput, 2);
                            Payment(accId, _checkedInput, out output, out didClose);
                            if (didClose == true)
                            {
                                if(!(AccountStore.StoredAccounts[accId]._AssociatedAccountId is null))
                                {
                                    AccountStore.StoredAccounts[AccountStore.StoredAccounts[accId]._AssociatedAccountId]._AssociatedAccountId = null;
                                }
                                AccCreator.AccountCloser(accId);
                                break;
                            }
                            Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId] +
                                "\nWould you like to pay more? (y/n)");
                            Parsers.YNCheck();
                            exit = Parsers.YNtoBool[Parsers.parsedYN];
                            if (!exit == true) { break; }
                        }
                        else { Parsers.InvalidInput(); }
                    }
                    else { Parsers.InvalidInput(); }
                }
            }
        }
        public static void Pay(string accId, decimal ammount)
        {
            TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 4, ammount);
            AccountBalance.Balance[accId] -= ammount;
        }
        public static void Payment(string accId, decimal ammount, out decimal output, out bool didClose)
        {
            if (AccountBalance.Balance[accId] <= ammount)
            {
                output = ammount - AccountBalance.Balance[accId];
                ammount = AccountBalance.Balance[accId];
                Pay(accId, ammount);
                Console.WriteLine("A payment of $" + ammount + " has been posted on account " + accId + "\nYour account has been payed off!");
                didClose = true;
            }
            else
            {
                Pay(accId, ammount);
                Console.WriteLine("A payment of $" + ammount + " has been posted on account " + accId + "\nYour current balance on this account is $" +
                    AccountBalance.Balance[accId]);
                output = 0;
                didClose = false;
            }
        }
        public static void OverdraftChecker(string accId, decimal input, out decimal _checkedInput, out bool _didClose)
        {
            bool didClose = false;
            string AssociatedaccId = AccountStore.StoredAccounts[accId]._AssociatedAccountId;
            decimal output;
            if (AssociatedaccId != null)
            {
                Console.WriteLine("There is an overdraft on this account for: $" + AccountBalance.Balance[AssociatedaccId]);
                Payment(AssociatedaccId, input, out output, out didClose);
                _checkedInput = output;
            }
            else
            {
                _checkedInput = input;
            }
            _didClose = didClose;
        }
        public static void Overdraft(string accId, decimal ammount)
        {
            if (AccountStore.StoredAccounts[accId].CanOverdraft)
            {
                Console.WriteLine("You have overdrafted on account: " + accId);
                if (AccountStore.StoredAccounts[accId]._AssociatedAccountId is null)
                {
                    ammount -= AccountBalance.Balance[accId];
                    TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 2, AccountBalance.Balance[accId]);
                    AccountBalance.Balance[accId] = 0;
                    AccCreator.OverCreator(accId, ammount);
                    Console.WriteLine("You now have an overdraft faucility: " + AccountStore.StoredAccounts[accId]._AssociatedAccountId +
                        " associated with account: " + accId + " for $" + ammount);
                }
                else
                {
                    string AaccId = AccountStore.StoredAccounts[accId]._AssociatedAccountId;
                    AccountBalance.Balance[AaccId] += ammount;
                    TransactionStore.AddTransferTransaction(AaccId, null, 4, 2, ammount);
                    Console.WriteLine("You now owe $" + AccountBalance.Balance[AccountStore.StoredAccounts[accId]._AssociatedAccountId] +
                        " on account: " + accId);
                }

            }
            else { Console.WriteLine("You cannot overdraft from a " + AccountStore.AccCharToName[accId[0]] + " account!"); }
        }
        public static void Overdraft(string accId, decimal ammount, out bool canOverdraft, out decimal deposit)
        {
            canOverdraft = true;
            if (AccountStore.StoredAccounts[accId].CanOverdraft)
            {
                Console.WriteLine("You have overdrafted on account: " + accId);
                if (AccountStore.StoredAccounts[accId]._AssociatedAccountId is null)
                {
                    deposit = ammount;
                    ammount -= AccountBalance.Balance[accId];
                    TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 2, AccountBalance.Balance[accId]);
                    AccountBalance.Balance[accId] = 0;
                    AccCreator.OverCreator(accId, ammount);
                    Console.WriteLine("You now have an overdraft faucility: " + AccountStore.StoredAccounts[accId]._AssociatedAccountId +
                        " associated with account: " + accId + " for $" + ammount);
                }
                else
                {
                    deposit = ammount;
                    string AaccId = AccountStore.StoredAccounts[accId]._AssociatedAccountId;
                    AccountBalance.Balance[AaccId] += ammount;
                    TransactionStore.AddTransferTransaction(AaccId, null, 4, 2, ammount);
                    Console.WriteLine("You now owe $" + AccountBalance.Balance[AccountStore.StoredAccounts[accId]._AssociatedAccountId] +
                        " on account: " + accId);
                }

            }
            else { 
                Console.WriteLine("You cannot overdraft from a " + AccountStore.AccCharToName[accId[0]] + " account!");
                canOverdraft = false;
                deposit = 0;
            }
        }
        public static void Withdraw(string accId)
        {
            Console.WriteLine("Note: enter a decimal number with no symbles. " +
                "Any decimal past the 2nd decimal place will be rounded up.");
            while (true)
            {
                bool exit = false;
                Console.WriteLine("How much would you like to withdraw?\nCurrent balance is: $" + AccountBalance.Balance[accId]);
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out _checkedInput);
                if (parsed && _checkedInput >= 0)
                {
                    _checkedInput = Math.Round(_checkedInput, 2);
                    if(AccountBalance.Balance[accId] - _checkedInput < 0)
                    {
                        Overdraft(accId, _checkedInput);
                        Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId] +
                            "\nWould you like to withdraw more? (y/n)");
                        Parsers.YNCheck();
                        exit = Parsers.YNtoBool[Parsers.parsedYN];
                        if (!exit == true) { break; }
                    }
                    else
                    {
                        AccountBalance.Balance[accId] -= _checkedInput;
                        TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 2, _checkedInput);
                        Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId] +
                            "\nWould you like to withdraw more? (y/n)");
                        Parsers.YNCheck();
                        exit = Parsers.YNtoBool[Parsers.parsedYN];
                        if (!exit == true) { break; }
                    }
                }
                else { Parsers.InvalidInput(); }
            }
        }
        public static void Withdraw(string accId, out decimal deposit)
        {
            Console.WriteLine("Note: enter a decimal number with no symbles. " +
                "Any decimal past the 2nd decimal place will be rounded up.");
            while (true)
            {
                Console.WriteLine("How much would you like to withdraw?\nCurrent balance is: $" + AccountBalance.Balance[accId]);
                input = Console.ReadLine();
                parsed = decimal.TryParse(input, out _checkedInput);
                if (parsed && _checkedInput >= 0)
                {
                    _checkedInput = Math.Round(_checkedInput, 2);
                    if (AccountBalance.Balance[accId] - _checkedInput < 0)
                    {
                        bool canOverdraft;
                        Overdraft(accId, _checkedInput, out canOverdraft, out deposit);
                        if (canOverdraft)
                        {
                            Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId]);
                        }
                        break;
                    }
                    else
                    {
                        AccountBalance.Balance[accId] -= _checkedInput;
                        TransactionStore.AddTransferTransaction(accId, null, AccountStore.AccCharToId[accId[0]], 2, _checkedInput);
                        Console.WriteLine("Your new balance is: $" + AccountBalance.Balance[accId]);
                        deposit = _checkedInput;
                        break;
                    }
                }
                else { Parsers.InvalidInput(); }
            }
        }
        public static void Transfer(string accId, string toAccId)
        {
            bool exit;
            decimal deposit;
            while (true)
            {
                Withdraw(accId, out deposit);
                if(deposit > 0)
                {
                    Deposit(toAccId, deposit);
                    Console.WriteLine("$" + deposit + " has been transered from " + accId + " to " + toAccId);
                }
                Console.WriteLine("\nWould you like to make another transfer? (y/n).");
                Parsers.YNCheck();
                exit = Parsers.YNtoBool[Parsers.parsedYN];
                if (!exit == true) { break; }
            }
            
        }
    }
}
