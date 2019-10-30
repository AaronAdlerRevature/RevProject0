using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    public class Transaction
    {
        public string _AccountId;
        public string _AssociatedAcountId;
        public int _AccountTypeId;
        public int _TransactionTypeId;      
        public decimal? _TransactionAmmount;
        public DateTime _TransactionDate = DateTime.Now;

        public Transaction(string AccountId, string AssociatedAcountId, int AccountTypeId, int TransactionTypeId, decimal? TransactionAmmount)
        {
            _AccountId = AccountId;
            _AssociatedAcountId = AssociatedAcountId;
            _AccountTypeId = AccountTypeId;
            _TransactionTypeId = TransactionTypeId;
            _TransactionAmmount = TransactionAmmount;
        }
    }

    static class TransactionStore
    {
        static Dictionary<long, Transaction> _StoredTransactions = new Dictionary<long, Transaction>();
        public static Dictionary<long, Transaction> StoredTransactions { get { return _StoredTransactions; } }
        public static long TransactionId = _StoredTransactions.Count;
        public readonly static Dictionary<int, string> TransactionTypeIds = new Dictionary<int, string>()
        {
            {0 , "Account created" },
            {1 , "Deposit" },
            {2 , "Withdraw" },
            {3 , "Transfer" },
            {4 , "Loan payment" },
            {5 , "Term Deposit matured" },
            {6 , "Interest accrued" },
            {7 , "Account closed" }
        };

        public static void AddTransferTransaction(string AccountId, string AssociatedAcountId, int AccountTypeId, int TransactionTypeId, decimal TransactionAmmount)
        {
            TransactionId = _StoredTransactions.Count;
            Transaction newTransaction = new Transaction(AccountId, AssociatedAcountId, AccountTypeId, TransactionTypeId, TransactionAmmount);
            _StoredTransactions.Add(TransactionId, newTransaction);
            Console.WriteLine("Transaction number: " + TransactionId + " has been created!");
        }

        public static void AddCloseTransaction(string AccountId)
        {
            TransactionId = _StoredTransactions.Count;
            int AccountTypeId = AccountStore.StoredAccounts[AccountId].typeId;
            Transaction newTransaction = new Transaction(AccountId, null, AccountTypeId, 7, null);
            _StoredTransactions.Add(TransactionId, newTransaction);
        } 
    }

    static class TransactionViewer
    {
        static List<string> ColumnNames = new List<string> 
        {
            "Id",
            "AccountId",
            "AssociatedId",
            "AccountTypeId",
            "TransactionType",
            "Ammount",
            "Date"
        };
        public static void Display()
        {
            Console.WriteLine("|{0,-10}|{1,-10}|{2,-15}|{3,-15}|{4,-20}|{5,-25}|{6,-25}|", 
                ColumnNames[0], ColumnNames[1], ColumnNames[2], ColumnNames[3], ColumnNames[4], ColumnNames[5], ColumnNames[6]);
            Console.WriteLine("|" + new string('-', 10) + "|" + new string('-', 10) + "|" + new string('-', 15) +
                "|" + new string('-', 15) + "|" + new string('-', 20) + "|" + new string('-', 25) + "|" + new string('-', 25) + "|");
            foreach (KeyValuePair<long, Transaction> entry in TransactionStore.StoredTransactions)
            {
                Console.WriteLine("|{0,-10}|{1,-10}|{2,-15}|{3,-15}|{4,-20}|{5,-25}|{6,-25}|", entry.Key, entry.Value._AccountId,
                    entry.Value._AssociatedAcountId, entry.Value._AccountTypeId, TransactionStore.TransactionTypeIds[entry.Value._TransactionTypeId], entry.Value._TransactionAmmount,
                    entry.Value._TransactionDate);
            }

        }
    }
}