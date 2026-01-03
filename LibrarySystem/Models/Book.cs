using System.Collections.Generic;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Status { get; set; } // "Available", "Borrowed", "Reserved"
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        // Logic Method inside Entity
        public bool IsAvailable()
        {
            return AvailableCopies > 0 && Status == "Available";
        }
    }
}