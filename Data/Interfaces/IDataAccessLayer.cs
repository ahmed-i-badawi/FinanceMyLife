using Syncfusion.Blazor;

namespace FinanceMyLife.Data.Interfaces
{
    public interface IDataAccessLayer<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(DataManagerRequest dm = default);
        Task<int> GetCountAsync();
        Task AddAsync(T obj);
        Task UpdateAsync(T obj, string pkName);
        //Task BatchUpdateAsync(T obj);
        Task RemoveAsync(object id);
    }
}