using System;
using System.Collections.Generic;
using System.Text;

namespace PlusDashData.Data
{
    public class UserSession
    {
        public Guid Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset? LastActivity { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
