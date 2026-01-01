using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public class Librarian : User
    {
        public string EmployeeId { get; set; }
        // لاحقًا يمكن إضافة List<Book> عند الربط بالـ Repository
    }
}

