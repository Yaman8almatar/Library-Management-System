using System;
using System.Collections.Generic;

namespace LibrarySystem.Models
{
    // === Abstract User ===
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime? JoinDate { get; set; }     // Optional for members
        public string EmployeeId { get; set; }      // Optional for librarians

        public string UserType { get; set; }
    }
}