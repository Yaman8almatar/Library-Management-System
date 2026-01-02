using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class FineRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Fine GetByLoan(int loanId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return null;
        }

        public Fine Create(Fine fine)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return fine;
        }

        public void Update(Fine fine)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }
    }
}