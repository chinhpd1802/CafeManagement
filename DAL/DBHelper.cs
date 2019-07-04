using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DBHelper
    {
        private static MySqlConnection connectionSTR;

        public static MySqlConnection GetConnection()
        {
            if (connectionSTR == null)
            {
                connectionSTR = new MySqlConnection
                {
                    ConnectionString = @"server = localhost; user id=root;port=3306;password=chinhhuy123;database=quanlycafe"
                };
            }
            return connectionSTR;
        }
        public static MySqlConnection OpenConnection()
        {
            if (connectionSTR == null)
            {
                GetConnection();
            }
            connectionSTR.Open();
            return connectionSTR;
        }
        public static void CloseConnection()
        {
            if (connectionSTR == null)
            {
                connectionSTR.Close();
            }
        }
        public static DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            try
            {
                using (GetConnection())
                {
                    OpenConnection();
                    MySqlCommand command = new MySqlCommand(query, connectionSTR);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(data);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return data;
        }
        public static int ExecuteNonQuery(string query)
        {
            int data = 0;
            try
            {
                using (GetConnection())
                {
                    OpenConnection();
                    MySqlCommand command = new MySqlCommand(query, connectionSTR);
                    data = command.ExecuteNonQuery();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
          public static object ExecuteScalar(string query)
        {
            object data = 0;
            try
            {
                using (GetConnection())
                {
                    OpenConnection();
                    MySqlCommand command = new MySqlCommand(query, connectionSTR);
                    data = command.ExecuteScalar();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
    }
}
