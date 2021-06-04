using System;

namespace PlusDashData.Data.Models.Content
{
    public class PersonalCard
    {
        public int Id { get; set; }
        public string CardDescription { get; set; }
        public int RowNo { get; set; }
        public int UserId { get; set; }
    }
}
