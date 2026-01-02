using System.Collections.Generic;
using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class LoanRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Loan Create(Loan loan)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return loan;
        }

        public void Update(Loan loan)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }

        public Loan GetById(int loanId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return null;
        }

        public List<Loan> GetActiveByMember(int memberId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return new List<Loan>();
        }
    }
}