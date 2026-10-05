using System;

namespace HIT_Campus_Housing_Portal.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Student" or "Admin"
        public DateTime CreatedAt { get; set; }
    }
}
