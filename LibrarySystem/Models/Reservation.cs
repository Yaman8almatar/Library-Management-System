using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; }
        public int QueuePosition { get; set; }

        // FK
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
