using System;

namespace PlusDashData.Data.Models.Accounts
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset? LastActivity { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public int UserId { get; set; }
    }
}
