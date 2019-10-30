using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetBankingApp.AaronAdler
{
    public interface IBalance
    {
        void CheckBalance(object AccountId);
    }

    public interface IAccounts
    {
        void OpenNew(int AccountTypeId, int CustomerId);
        void Close(int AccountId, int CustomerId);
        void DisplayAccounts(int CustomerId);

    }

    public interface ICustomerRegistration
    {
        void Register(int CustomerId, string FirstName, string LastName, string Address);
        //Cannot delete customer, as want to keep transaction history archive.
    }

    public interface ITransactions
    {
        void Withdraw(int FullAccountId, int Ammount);
        void Deposit(int FullAccountId, int Ammount);
        void Transfer(int FullAccountId, int AssociatedAccountId, int Ammount);
        void PayLoanInstallment();
    
        void DisplayTransactions();
    }

    

}
