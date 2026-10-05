using System;
using System.Data;
using HIT_Campus_Housing_Portal.Models;
using HIT_Campus_Housing_Portal.Services;
using MySql.Data.MySqlClient;

namespace HIT_Campus_Housing_Portal.Data.Repositories
{
    public class UserRepository
    {
        public User Authenticate(string username, string password)
        {
            string hash = PasswordHasher.HashPassword(password);
            string sql = "SELECT user_id, username, password_hash, role, created_at FROM users WHERE LOWER(username) = LOWER(@username);";
            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@username", username));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string storedHash = row["password_hash"].ToString();

                // Check hashed password match or legacy plaintext match fallback
                if (PasswordHasher.VerifyPassword(password, storedHash) || string.Equals(password, storedHash, StringComparison.Ordinal))
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(row["user_id"]),
                        Username = row["username"].ToString(),
                        PasswordHash = storedHash,
                        Role = row["role"].ToString(),
                        CreatedAt = Convert.ToDateTime(row["created_at"])
                    };
                }
            }

            return null;
        }

        public int CreateUser(string username, string password, string role = "Student")
        {
            string hash = PasswordHasher.HashPassword(password);
            string sql = @"INSERT INTO users (username, password_hash, role) 
                           VALUES (@username, @password_hash, @role);
                           SELECT LAST_INSERT_ID();";

            object result = DbConnectionFactory.ExecuteScalar(sql,
                new MySqlParameter("@username", username),
                new MySqlParameter("@password_hash", hash),
                new MySqlParameter("@role", role));

            return Convert.ToInt32(result);
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            string hash = PasswordHasher.HashPassword(newPassword);
            string sql = "UPDATE users SET password_hash = @hash WHERE user_id = @userId;";
            int rows = DbConnectionFactory.ExecuteNonQuery(sql,
                new MySqlParameter("@hash", hash),
                new MySqlParameter("@userId", userId));
            return rows > 0;
        }

        public bool DeleteUser(int userId)
        {
            string sql = "DELETE FROM users WHERE user_id = @userId;";
            int rows = DbConnectionFactory.ExecuteNonQuery(sql, new MySqlParameter("@userId", userId));
            return rows > 0;
        }

        public bool UserExists(string username)
        {
            string sql = "SELECT COUNT(*) FROM users WHERE LOWER(username) = LOWER(@username);";
            object count = DbConnectionFactory.ExecuteScalar(sql, new MySqlParameter("@username", username));
            return Convert.ToInt32(count) > 0;
        }
    }
}
