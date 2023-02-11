using FinanceMyLife.Data.Models;

namespace FinanceMyLife.Data.Interfaces
{
    public interface ICustomCRUDData<T> where T : class
    {
        Task AddBatchTransactions(TransactionFModel fmodel);
    }
}