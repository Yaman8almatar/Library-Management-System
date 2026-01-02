using System.Collections.Generic;
using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class BookRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Book GetById(int bookId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return null;
        }

        public List<Book> Search(string criteria)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return new List<Book>();
        }

        public Book Add(Book book)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return book;
        }

        public void Update(Book book)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }

        public void Delete(int bookId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }

        public bool DecreaseAvailable(int bookId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
            return true;
        }

        public void IncreaseAvailable(int bookId)
        {
            using SqlConnection con = db.GetConnection();
            con.Open();
        }
    }
}