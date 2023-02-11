namespace FinanceMyLife.Data.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRefundable { get; set; }

        public List<Transaction>? Transactions { get; set; }

    }
}
