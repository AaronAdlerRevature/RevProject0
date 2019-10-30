using System;
using System.Data.SqlClient;
using System.Linq;

namespace NetBankingApp.AaronAdler
{
    class Customer
    {
        string _Username;
        string _Password;

        public Customer(string Username, string Password)
        {
            this._Username = Username;
            this._Password = Password;
        }
    }
}

    