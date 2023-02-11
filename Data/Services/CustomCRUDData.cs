using FinanceMyLife.Data.Entities;
using FinanceMyLife.Data.Enums;
using FinanceMyLife.Data.Interfaces;
using FinanceMyLife.Data.Models;

namespace FinanceMyLife.Data.Services;

public class CustomCRUDData<T> : ICustomCRUDData<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly object _lock = new object();

    public CustomCRUDData(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Transactions

    private DateTime GetNextDate(DateTime startDate, PeriodsTypesEnum periodType, int i)
    {
        switch (periodType)
        {
            case PeriodsTypesEnum.Daily:
                return startDate.AddDays(i);
            case PeriodsTypesEnum.Weekly:
                return startDate.AddDays(7 * i);
            case PeriodsTypesEnum.Monthly:
                return startDate.AddMonths(i);
            case PeriodsTypesEnum.Anually:
                return startDate.AddYears(i);
            default:
                throw new ArgumentException("Invalid period type");
        }
    }

    private async Task<List<Transaction>> GenerateTransactionBatchToAdd(TransactionFModel fmodel)
    {
        List<Transaction> transactions = new List<Transaction>();

        for (int i = 0; i < fmodel.NoOfTimes; i++)
        {
            transactions.Add(new Transaction
            {
                Date = GetNextDate(fmodel.StartDate, fmodel.PeriodType, i),
                Description = fmodel.Description,
                Amount = fmodel.Category.Type == TransactionTypeEnum.Expenses ? fmodel.Amount * -1 : fmodel.Amount,
                CategoryId = fmodel.CategoryId,
                MemberId = fmodel.MemberId,
                Number = fmodel.IsOrdered ? i + 1 : 0
            });
        }

        return transactions;
    }


    public async Task AddBatchTransactions(TransactionFModel fmodel)
    {
        var dbTransactions = await GenerateTransactionBatchToAdd(fmodel);

        await _context.Set<Transaction>().AddRangeAsync(dbTransactions);
        await _context.SaveChangesAsync();
    }

    #endregion


}