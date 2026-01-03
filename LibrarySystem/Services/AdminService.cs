using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services
{
    public class AdminService
    {
        private BookRepository _bookRepo = new BookRepository();
        private UserRepository _userRepo = new UserRepository();

        public void AddBook(Book book)
        {
            // منطق إضافة كتاب جديد عبر المستودع
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Books (Title, Author, Year, Status, TotalCopies, AvailableCopies) VALUES (@t, @a, @y, @s, @tc, @ac)";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", book.Title);
                    cmd.Parameters.AddWithValue("@a", book.Author);
                    cmd.Parameters.AddWithValue("@y", book.Year);
                    cmd.Parameters.AddWithValue("@s", "Available");
                    cmd.Parameters.AddWithValue("@tc", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@ac", book.TotalCopies);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ManageUser(int userId, string action)
        {
            if (action == "Delete")
            {
                // تنفيذ حذف مستخدم
            }
            // يمكن إضافة تعديل البيانات هنا
        }
    }
}