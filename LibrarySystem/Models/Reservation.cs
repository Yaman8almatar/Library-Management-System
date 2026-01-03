using System;

namespace LibrarySystem.Models
{
    public class Reservation
    {
        public int ResId { get; set; }
        public int UserId { get; set; } // FK Linking
        public int BookId { get; set; } // FK Linking
        public DateTime ResDate { get; set; }
        public string Status { get; set; }
    }
}