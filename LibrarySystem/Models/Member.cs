using System;
using System.Collections.Generic;

namespace LibrarySystem.Models
{
    public class Member : User
    {
        public DateTime JoinDate { get; set; }

        public Member()
        {
            UserType = "Member";
        }
    }
}