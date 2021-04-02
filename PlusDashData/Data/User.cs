using System;

namespace TaskManager.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool ValidatedEmail { get; set; }
        public string Password { get; set; }
        public int  LoginAttempts { get; set; }
        public DateTime LockOutEnd { get; set; }
        public bool AccountAccess { get; set; }
    }
}
