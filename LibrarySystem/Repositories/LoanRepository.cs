using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class LoanRepository
    {
        public void Create(Loan loan)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Loans (UserId, BookId, StartDate, DueDate, Status) VALUES (@uid,@bid,@start,@due,@status)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", loan.UserId);
                    cmd.Parameters.AddWithValue("@bid", loan.BookId);
                    cmd.Parameters.AddWithValue("@start", loan.StartDate);
                    cmd.Parameters.AddWithValue("@due", loan.DueDate);
                    cmd.Parameters.AddWithValue("@status", loan.Status ?? "Active");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Loan GetById(int loanId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Loans WHERE LoanId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", loanId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Loan
                            {
                                LoanId = (int)reader["LoanId"],
                                UserId = (int)reader["UserId"],
                                BookId = (int)reader["BookId"],
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ReturnDate"]),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Loan> GetByUser(int userId)
        {
            List<Loan> loans = new List<Loan>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Loans WHERE UserId=@uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loans.Add(new Loan
                            {
                                LoanId = (int)reader["LoanId"],
                                UserId = (int)reader["UserId"],
                                BookId = (int)reader["BookId"],
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                ReturnDate = reader["ReturnDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ReturnDate"]),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return loans;
        }

        public void CloseLoan(int loanId, DateTime returnDate)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Loans SET ReturnDate=@ret, Status='Closed' WHERE LoanId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ret", returnDate);
                    cmd.Parameters.AddWithValue("@id", loanId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDueDate(int loanId, DateTime newDueDate)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Loans SET DueDate=@due WHERE LoanId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@due", newDueDate);
                    cmd.Parameters.AddWithValue("@id", loanId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}