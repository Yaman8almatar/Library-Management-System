using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class UserRepository
    {
        public void Add(User user)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Users (Name, Username, PasswordHash, Email, UserType, JoinDate, EmployeeId) " +
                               "VALUES (@name, @uname, @pass, @email, @type, @join, @emp)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@uname", user.Username);
                    cmd.Parameters.AddWithValue("@pass", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@email", user.Email ?? "");
                    cmd.Parameters.AddWithValue("@type", user.UserType);
                    cmd.Parameters.AddWithValue("@join", user.JoinDate ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@emp", user.EmployeeId ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(User user)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET Name=@name, PasswordHash=@pass, Email=@email WHERE UserId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@pass", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@email", user.Email ?? "");
                    cmd.Parameters.AddWithValue("@id", user.UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int userId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE UserId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetByUsername(string username)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@uname";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uname", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // مثال: إذا UserType = "Member" نرجع Member
                            string type = reader["UserType"].ToString();
                            User u;
                            if (type == "Librarian")
                                u = new Librarian();
                            else
                                u = new Member();

                            u.UserId = (int)reader["UserId"];
                            u.Name = reader["Name"].ToString();
                            u.Username = reader["Username"].ToString();
                            u.PasswordHash = reader["PasswordHash"].ToString();
                            u.UserType = type;
                            u.Email = reader["Email"].ToString();
                            u.JoinDate = reader["JoinDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["JoinDate"]);
                            u.EmployeeId = reader["EmployeeId"].ToString();
                            return u;
                        }
                    }
                }
            }
            return null;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Users";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["UserType"].ToString();
                            User u;
                            if (type == "Librarian")
                                u = new Librarian();
                            else
                                u = new Member();

                            u.UserId = (int)reader["UserId"];
                            u.Name = reader["Name"].ToString();
                            u.Username = reader["Username"].ToString();
                            u.PasswordHash = reader["PasswordHash"].ToString();
                            u.UserType = type;
                            u.Email = reader["Email"].ToString();
                            u.JoinDate = reader["JoinDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["JoinDate"]);
                            u.EmployeeId = reader["EmployeeId"].ToString();
                            users.Add(u);
                        }
                    }
                }
            }
            return users;
        }
    }
}