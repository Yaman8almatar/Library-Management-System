using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class UserRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public User GetById(int userId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            // تنفيذ Select لاحقًا
            return null;
        }

        public User GetByUsername(string username)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return null;
        }

        public User Add(User user)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return user;
        }

        public void Update(User user)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }

        public void Delete(int userId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }
    }
}