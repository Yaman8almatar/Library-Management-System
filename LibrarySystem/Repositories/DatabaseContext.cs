using System.Data.SqlClient;

namespace LibrarySystem.Repositories
{
    public class DatabaseContext
    {
        private readonly string connectionString =
            "Server=DESKTOP-44HJOQC\\MSSQLSERVERRR;Database=LibraryDB;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}