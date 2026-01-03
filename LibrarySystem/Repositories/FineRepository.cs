using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using System;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class FineRepository
    {
        // إنشاء غرامة جديدة (تُستدعى عند إرجاع كتاب متأخر)
        public void Create(Fine fine)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Fines (LoanId, Amount, PaymentStatus) VALUES (@lid, @amt, @status)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", fine.LoanId);
                    cmd.Parameters.AddWithValue("@amt", fine.Amount);
                    cmd.Parameters.AddWithValue("@status", "Unpaid");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // جلب غرامة مرتبطة بإعارة معينة
        public Fine GetByLoan(int loanId)
        {
            Fine fine = null;
            using (var conn =   LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Fines WHERE LoanId = @lid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@lid", loanId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fine = new Fine
                            {
                                FineId = (int)reader["FineId"],
                                LoanId = (int)reader["LoanId"],
                                Amount = (decimal)reader["Amount"],
                                PaymentStatus = reader["PaymentStatus"].ToString()
                            };
                        }
                    }
                }
            }
            return fine;
        }

        // تحديث حالة الدفع (UC-007)
        public void UpdateStatus(int fineId, string status)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Fines SET PaymentStatus = @s WHERE FineId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", fineId);
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}