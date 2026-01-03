namespace LibrarySystem.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int LoanId { get; set; } // FK Linking
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // "Unpaid", "Paid"
    }
}