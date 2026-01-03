using System;

namespace LibrarySystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; } // FK Linking
        public int BookId { get; set; } // FK Linking
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable
        public string Status { get; set; } // "Active", "Closed", "Overdue"
    }
}