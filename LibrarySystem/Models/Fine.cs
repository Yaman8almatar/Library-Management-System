using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        // FK
        public int LoanId { get; set; }
    }
}
