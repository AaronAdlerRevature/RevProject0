using System;
using System.Data.SqlClient;

namespace NetBankingApp.AaronAdler
{
    public static class DbCommuner
    {
        static string connectString = @"Server= DESKTOP-2PO57TR\SQLEXPRESS; Database= NetBankingApp;Integrated Security = SSPI;";
        static bool isConnected = false;
        static SqlConnection _NetBankConn = new SqlConnection(connectString);
        public static SqlConnection NetBankConn { get { return _NetBankConn; } }

        public static void SetConn()
        {
            _NetBankConn = new SqlConnection(connectString);
        }
        public static void Connect()
        {
            try
            {
                _NetBankConn.Open();
                Console.WriteLine("Connection Success!");
                isConnected = true;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine("Connection Failed!");
                Console.WriteLine(ex.Message + "\n\n\n" + ex.StackTrace);
            }
        }
        public static void Disconnect()
        {
            try
            {
                _NetBankConn.Close();
                isConnected = false;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Console.WriteLine("Could not close the connection.\n" + ex.Message + "\n\n" + ex.StackTrace);
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details.", Justification = "<Pending>")]
        public static void InsertData(string Username, string Password)
        {
            DbCommuner.SetConn();
            DbCommuner.Connect();
            string UserInsertQuery = "INSERT INTO UserRegistration(Username, Password) VALUES (@username,@password)";
            using (DbCommuner.NetBankConn)
            {
                using (SqlCommand QueryUserInsert = new SqlCommand(UserInsertQuery, DbCommuner.NetBankConn))
                {
                    QueryUserInsert.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = Username;
                    QueryUserInsert.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 100).Value = Password;
                    try
                    {
                        QueryUserInsert.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n\n" + ex.StackTrace);
                        throw ex;
                    }
                    
                }
            }
            DbCommuner.Disconnect();
        }
    }
}
