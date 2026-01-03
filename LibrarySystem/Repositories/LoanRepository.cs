using Microsoft.Data.SqlClient;
using LibrarySystem.Models;
using LibrarySystem.Data;

namespace LibrarySystem.Repositories
{
    public class LoanRepository
    {
        public void Create(Loan loan)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Loans (UserId, BookId, StartDate, DueDate, Status) 
                                 VALUES (@uid, @bid, @start, @due, @stat)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    // هنا يتم الربط الفعلي: نرسل ID المستخدم و ID الكتاب للداتا بيس
                    cmd.Parameters.AddWithValue("@uid", loan.UserId);
                    cmd.Parameters.AddWithValue("@bid", loan.BookId);
                    cmd.Parameters.AddWithValue("@start", loan.StartDate);
                    cmd.Parameters.AddWithValue("@due", loan.DueDate);
                    cmd.Parameters.AddWithValue("@stat", "Active");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 1. دالة لجلب استعارة بالـ ID (يحتاجها كود الإرجاع والتجديد)
        public Loan GetById(int loanId)
        {
            Loan loan = null;
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Loans WHERE LoanId = @id";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", loanId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            loan = new Loan
                            {
                                LoanId = (int)reader["LoanId"],
                                UserId = (int)reader["UserId"],
                                BookId = (int)reader["BookId"],
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return loan;
        }

        // 2. دالة لإغلاق الاستعارة (تحديث تاريخ الإرجاع)
        public void CloseLoan(int loanId, DateTime returnDate)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Loans SET ReturnDate = @rd, Status = 'Closed' WHERE LoanId = @id";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", loanId);
                    cmd.Parameters.AddWithValue("@rd", returnDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 3. دالة لتحديث تاريخ الاستحقاق (للتجديد)
        public void UpdateDueDate(int loanId, DateTime newDate)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Loans SET DueDate = @nd WHERE LoanId = @id";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", loanId);
                    cmd.Parameters.AddWithValue("@nd", newDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}