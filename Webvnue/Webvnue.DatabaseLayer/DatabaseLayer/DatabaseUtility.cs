using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Webvnue.DatabaseLayer.DatabaseLayer
{
    public class DatabaseUtility
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["webvnue"].ConnectionString;
        private static SqlConnection connection = new SqlConnection(connectionString);

        public static void insertAccount(string id, string email, string password, string firstname, string lastname, DateTime? dob)
        {
            string storedProcedure = "INSERT_ACCOUNT";
            SqlCommand cmd = new SqlCommand(storedProcedure, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@dob", dob);

            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static bool verifyAccount(string email, string password)
        {
            string storedProcedure = "VERIFY_ACCOUNT";
            SqlCommand cmd = new SqlCommand(storedProcedure, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            bool result = false;

            connection.Open();

            if (cmd.ExecuteScalar() != null)
            {
                result = true;
            }

            connection.Close();

            return result;
        }

    }
}