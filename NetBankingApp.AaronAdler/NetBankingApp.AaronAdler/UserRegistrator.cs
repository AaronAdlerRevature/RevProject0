using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace NetBankingApp.AaronAdler
{
    public static class UserRegistrator
    {
        static bool _IsUnique;
        public static bool IsUnique { get { return _IsUnique; } }
        static string _Username;
        static string _Password;
        static decimal cash;
        public static string Username { get { return _Username; } }
        public static string Password { get { return _Password; } }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to NetBanking customer registration!\nPlease enter a username and password for your account.\n" +
                "Note that a username must be at least 3 characters long, and password must be at least 8.");
        }
        public static void getUsername()
        {
            while (true)
            {
                Console.Write("Please enter a valid username: ");
                _Username = Console.ReadLine();
                if (Username != null && Username.Length > 2)
                {
                    Console.WriteLine("\n{0} is a valid username.\n", Username);
                    break;
                }
                else
                {
                    Console.WriteLine("\nUsername {0} is too short!\n", Username);
                }
            }
        }
        public static void VerifyUniqueUserDb()
        {
            string usernameQuery = "SELECT UserName FROM UserRegistration WHERE UserName = '" + _Username + "'";
            using (DbCommuner.NetBankConn)
            {
                void checkUsername()
                {
                    DbCommuner.SetConn();
                    DbCommuner.Connect();
                    SqlCommand QueryUsername = new SqlCommand(usernameQuery, DbCommuner.NetBankConn);
                    using (SqlDataReader reader = QueryUsername.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("The username is not unique");
                            _IsUnique = false;
                        }
                        else
                        {
                            Console.WriteLine("The username is unique");
                            _IsUnique = true;
                        }
                        reader.Close();
                    }
                }
                checkUsername();
                DbCommuner.Disconnect();
            }
        }
        public static void getPassword()
        {
            while (true)
            {
                Console.Write("Please enter a valid password: ");
                _Password = Console.ReadLine();
                if (Password != null && Password.Length > 5)
                {
                    Console.WriteLine("\n{0} is a valid password.\n", Password);
                    break;
                }
                else
                {
                    Console.WriteLine("\nPassword {0} is too short!\n", Password);
                }
            }

        }

    }
}
