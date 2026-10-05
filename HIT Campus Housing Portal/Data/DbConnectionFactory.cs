using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace HIT_Campus_Housing_Portal.Data
{
    public static class DbConnectionFactory
    {
        private static string GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["HIT_CHP_MySQL"] != null)
            {
                return ConfigurationManager.ConnectionStrings["HIT_CHP_MySQL"].ConnectionString;
            }

            // Default fallback connection string for MySQL 8.0 on localhost
            return "Server=localhost;Port=3306;Database=hit_chp_db;Uid=root;Pwd=;SslMode=Preferred;";
        }

        public static MySqlConnection GetConnection()
        {
            string connStr = GetConnectionString();
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var conn = GetConnection())
                {
                    return conn.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable ExecuteTable(string sql, params MySqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
