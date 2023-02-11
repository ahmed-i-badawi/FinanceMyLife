using FinanceMyLife.Data.Enums;

namespace FinanceMyLife.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
