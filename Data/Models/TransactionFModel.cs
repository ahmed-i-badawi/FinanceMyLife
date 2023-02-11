using FinanceMyLife.Data.Entities;
using FinanceMyLife.Data.Enums;

namespace FinanceMyLife.Data.Models
{
    public class TransactionFModel
    {
        public string Description { get; set; }
        public double Amount { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? MemberId { get; set; }
        public Member? Member { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public PeriodsTypesEnum PeriodType { get; set; } = PeriodsTypesEnum.Daily;
        public int NoOfTimes { get; set; } = 1;
        public DateTime EndDate { get; set; }
        public bool IsOrdered { get; set; }
    }
}
