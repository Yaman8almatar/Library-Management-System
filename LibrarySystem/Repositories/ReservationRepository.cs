using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using System;
using System.Collections.Generic;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class ReservationRepository
    {
        public void Add(Reservation res)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Reservations (UserId, BookId, ResDate, Status) VALUES (@uid, @bid, @date, @stat)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", res.UserId);
                    cmd.Parameters.AddWithValue("@bid", res.BookId);
                    cmd.Parameters.AddWithValue("@date", res.ResDate);
                    cmd.Parameters.AddWithValue("@stat", res.Status ?? "Pending");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int resId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Reservations WHERE ResId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", resId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Reservation> GetByUser(int userId)
        {
            var list = new List<Reservation>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Reservations WHERE UserId = @uid";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToReservation(reader));
                        }
                    }
                }
            }
            return list;
        }

        public List<Reservation> GetAll()
        {
            var list = new List<Reservation>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Reservations";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToReservation(reader));
                        }
                    }
                }
            }
            return list;
        }

        private Reservation MapReaderToReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                ResId = (int)reader["ResId"],
                UserId = (int)reader["UserId"],
                BookId = (int)reader["BookId"],
                ResDate = reader["ResDate"] != DBNull.Value ? Convert.ToDateTime(reader["ResDate"]) : DateTime.MinValue,
                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Pending"
            };
        }
    }
}