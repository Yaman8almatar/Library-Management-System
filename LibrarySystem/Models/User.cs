using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Models
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}

