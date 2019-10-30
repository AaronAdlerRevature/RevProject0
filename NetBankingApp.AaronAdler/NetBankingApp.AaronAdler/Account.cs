using System;
using System.Collections.Generic;
using System.Text;

namespace NetBankingApp.AaronAdler
{
    abstract class Account
    {
        protected string _AccountId;
        private DateTime _CreationTime = DateTime.Now;
        protected int _typeId;                     //(Checking, Business, Loan, TermDeposit, OverdraftFacility) 0-4 respectively, will have a fetch name method
        protected bool _CanOverdraft = false;
        protected bool _CanTranfer = true;
        protected decimal _InterestRate = .0006M;
        protected int _InstallmentTypeId = 0;
        public string _AssociatedAccountId;
        protected decimal? _LoanAmount = null;

        public string AccountId { get { return _AccountId; } }
        public DateTime CreationTime { get { return _CreationTime; } }
        public int typeId { get { return _typeId; } }
        public bool CanOverdraft { get { return _CanOverdraft; } }
        public bool CanTransfer { get { return _CanTranfer; } }
        public decimal InterestRate { get { return _InterestRate; } }
        public int InstallmentTypeId { get { return _InstallmentTypeId; } }



    }



    class Checking : Account
    {
        public Checking(string AccountId)
        {
            _typeId = 0;
            _AccountId = AccountId;
        }

    }

    class Business : Account
    {
        public Business(string AccountId)
        {
            _typeId = 1;
            _AccountId = AccountId;
            _CanOverdraft = true;
        }

    }

    class OverdraftFaucility : Account
    {

        public OverdraftFaucility(string AccountId, string AssociatedAccountId, decimal ammount)
        {
            _typeId = 4;
            _AccountId = AccountId;
            _AssociatedAccountId = AssociatedAccountId;
            _CanTranfer = false;
            AccountBalance.Balance[AccountId] = ammount;
        }
    }

    class Loan : Account
    {
        public Loan(string AccountId, decimal InterestRate, int InstallmentTypeId, decimal LoanAmount)
        {
            _typeId = 2;
            _CanTranfer = false;
            _AccountId = AccountId;
            _InterestRate = InterestRate;
            _InstallmentTypeId = InstallmentTypeId;
            _LoanAmount = LoanAmount;
        }
    }

    class TermDeposit : Account
    {
        public TermDeposit(string AccountId, decimal InterestRate)
        {
            _typeId = 3;
            _CanTranfer = false;
            _AccountId = AccountId;
            _InterestRate = InterestRate;
        }
    }
}
