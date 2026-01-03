using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;
using Microsoft.Data.SqlClient;

namespace LibrarySystem.Data
{
   
        public class LibraryDbContext
        {
            // غير هذا السطر ليناسب إعدادات جهازك
            private static string _connectionString = "Server=DESKTOP-44HJOQC\\MSSQLSERVERRR;Database=LibraryDB;Trusted_Connection=True;";

            public static SqlConnection GetConnection()
            {
                return new SqlConnection(_connectionString);
            }
        }
  }
