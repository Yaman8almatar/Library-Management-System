using Microsoft.Data.SqlClient;
using LibrarySystem.Models;
using LibrarySystem.Data;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Repositories
{
    public class ReservationRepository
    {
        public void Create(Reservation res)
        {
            using (var conn =   LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Reservations (UserId, BookId, ResDate, Status) VALUES (@uid, @bid, @date, @stat)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", res.UserId);
                    cmd.Parameters.AddWithValue("@bid", res.BookId);
                    cmd.Parameters.AddWithValue("@date", res.ResDate);
                    cmd.Parameters.AddWithValue("@stat", "Pending");
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
    }
}