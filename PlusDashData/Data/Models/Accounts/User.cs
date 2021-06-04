namespace PlusDashData.Data.Models.Accounts
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool ValidatedEmail { get; set; }
        public string Password { get; set; }
    }
}
