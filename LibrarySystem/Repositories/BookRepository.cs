using Microsoft.Data.SqlClient;
using LibrarySystem.Data;
using System;
using System.Collections.Generic;
using LibrarySystem.Models;

namespace LibrarySystem.Repositories
{
    public class BookRepository
    {
        // ??????? ???? ???? ????? ?????? ?? ????? CS1061
        public List<Book> SearchByTitle(string title)
        {
            List<Book> books = new List<Book>();
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                // ?????? LIKE ????? ?? ????? ???? ????? ??? ??? ?? ????
                string query = "SELECT * FROM Books WHERE Title LIKE @t";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", "%" + title + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                BookId = (int)reader["BookId"],
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                AvailableCopies = (int)reader["AvailableCopies"],
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return books;
        }

        // ????? ??? ???? ???? ID (???? ?? ??????)
        public Book GetById(int id)
        {
            Book book = null;
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE BookId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book = new Book
                            {
                                BookId = (int)reader["BookId"],
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                AvailableCopies = (int)reader["AvailableCopies"],
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return book;
        }

        // ????? ??????? ???? ??????? AdminService
        public void Add(Book book)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Books (Title, Author, Year, Status, TotalCopies, AvailableCopies) 
                                 VALUES (@t, @a, @y, @s, @tc, @ac)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", book.Title);
                    cmd.Parameters.AddWithValue("@a", book.Author);
                    cmd.Parameters.AddWithValue("@y", book.Year);
                    cmd.Parameters.AddWithValue("@s", book.Status ?? "Available");
                    cmd.Parameters.AddWithValue("@tc", book.TotalCopies);
                    cmd.Parameters.AddWithValue("@ac", book.AvailableCopies);
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

        public void UpdateStatus(int bookId, string status)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Books SET Status = @s WHERE BookId = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // ???? ?????? ??? ????? (?????? ??? ????? ??????)
        public void IncreaseAvailable(int bookId)
        {
            using (var conn = LibraryDbContext.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookId = @id";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}