using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using System;
using System.Collections.Generic;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class BookRepository
    {
        public List<Book> SearchByTitle(string title)
        {
            var books = new List<Book>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE Title LIKE @t AND [Status] <> 'Deleted'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", "%" + title + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        public Book GetById(int id)
        {
            Book book = null;
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE BookId = @id AND [Status] <> 'Deleted'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book = MapReaderToBook(reader);
                        }
                    }
                }
            }
            return book;
        }

        public List<Book> GetAll()
        {
            var books = new List<Book>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE [Status] <> 'Deleted'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        public void Add(Book book)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Books (Title, Author, [Year], [Status], TotalCopies, AvailableCopies) 
                                 VALUES (@t, @a, @y, @s, @tc, @ac)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", book.Title);
                    cmd.Parameters.AddWithValue("@a", book.Author ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@y", book.Year);
                    cmd.Parameters.AddWithValue("@s", string.IsNullOrEmpty(book.Status) ? "Available" : book.Status);
                    cmd.Parameters.AddWithValue("@tc", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@ac", book.AvailableCopies);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Book book)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE Books SET Title=@t, Author=@a, [Year]=@y, [Status]=@s, TotalCopies=@tc, AvailableCopies=@ac
                                 WHERE BookId=@id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", book.Title);
                    cmd.Parameters.AddWithValue("@a", book.Author ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@y", book.Year);
                    cmd.Parameters.AddWithValue("@s", book.Status ?? "Available");
                    cmd.Parameters.AddWithValue("@tc", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@ac", book.AvailableCopies);
                    cmd.Parameters.AddWithValue("@id", book.BookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int bookId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Books WHERE BookId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DecreaseAvailable(int bookId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookId = @id AND AvailableCopies > 0";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void IncreaseAvailable(int bookId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStatus(int bookId, string status)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Books SET [Status] = @s WHERE BookId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Book MapReaderToBook(SqlDataReader reader)
        {
            return new Book
            {
                BookId = (int)reader["BookId"],
                Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : null,
                Author = reader["Author"] != DBNull.Value ? reader["Author"].ToString() : null,
                Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"]) : 0,
                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Available",
                TotalCopies = reader["TotalCopies"] != DBNull.Value ? Convert.ToInt32(reader["TotalCopies"]) : 0,
                AvailableCopies = reader["AvailableCopies"] != DBNull.Value ? Convert.ToInt32(reader["AvailableCopies"]) : 0
            };
        }
    }
}