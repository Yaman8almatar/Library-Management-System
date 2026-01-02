using System.Collections.Generic;
using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class ReservationRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Reservation Create(Reservation reservation)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return reservation;
        }

        public List<Reservation> GetByBook(int bookId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return new List<Reservation>();
        }

        public void Delete(int reservationId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }
    }
}