using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // nullable لأن الكتاب لم يرجع بعد
        public string Status { get; set; }
        public int RenewCount { get; set; }

        // FK
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
