using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public class Member : User
    {
        public DateTime JoinDate { get; set; }
        // لاحقًا يمكن إضافة List<Loan> أو List<Reservation> عند الربط بالـ Repository
    }
}

