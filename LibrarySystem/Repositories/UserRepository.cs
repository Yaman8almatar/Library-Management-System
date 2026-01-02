using Microsoft.Data.SqlClient;
using LibrarySystem.Models;
using LibrarySystem.Data;

namespace LibrarySystem.Repositories
{
    public class UserRepository
    {
        public User GetByUsername(string username)
        {
            User user = null;
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @u";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // تحديد نوع المستخدم لإنشاء الكائن المناسب
                            string type = reader["UserType"].ToString();
                            if (type == "Member")
                            {
                                user = new Member { JoinDate = Convert.ToDateTime(reader["JoinDate"]) };
                            }
                            else
                            {
                                user = new Librarian { EmployeeId = reader["EmployeeId"].ToString() };
                            }

                            // تعبئة البيانات المشتركة
                            user.UserId = (int)reader["UserId"];
                            user.Name = reader["Name"].ToString();
                            user.Username = reader["Username"].ToString();
                            user.PasswordHash = reader["PasswordHash"].ToString();
                        }
                    }
                }
            }
            return user;
        }

        public void Add(User user)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Users (Name, Username, PasswordHash, UserType) VALUES (@n, @u, @p, @t)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@n", user.Name);
                    cmd.Parameters.AddWithValue("@u", user.Username);
                    cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@t", user.UserType);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}