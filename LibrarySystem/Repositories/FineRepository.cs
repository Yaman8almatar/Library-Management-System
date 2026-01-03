using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class FineRepository
    {
        public void Create(Fine fine)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Fines (LoanId, Amount, PaymentStatus) VALUES (@loan,@amt,@status)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@loan", fine.LoanId);
                    cmd.Parameters.AddWithValue("@amt", fine.Amount);
                    cmd.Parameters.AddWithValue("@status", fine.PaymentStatus ?? "Unpaid");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Fine> GetByUser(int userId)
        {
            List<Fine> fines = new List<Fine>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = @"SELECT f.* FROM Fines f
                                 INNER JOIN Loans l ON f.LoanId=l.LoanId
                                 WHERE l.UserId=@uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fines.Add(new Fine
                            {
                                FineId = (int)reader["FineId"],
                                LoanId = (int)reader["LoanId"],
                                Amount = (decimal)reader["Amount"],
                                PaymentStatus = reader["PaymentStatus"].ToString()
                            });
                        }
                    }
                }
            }
            return fines;
        }

        public void UpdateStatus(int fineId, string newStatus)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Fines SET PaymentStatus=@status WHERE FineId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", fineId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}