namespace FinanceMyLife.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? MemberId { get; set; }
        public Member? Member { get; set; }

        public DateTime Date { get; set; }
        public int Number { get; set; }
    }
}
