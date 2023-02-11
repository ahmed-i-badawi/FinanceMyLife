using FinanceMyLife.Data.Enums;
using FinanceMyLife.Data.Interfaces;
using FinanceMyLife.Extensions;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace FinanceMyLife.Data.Services;

public class DataAccessLayer<T> : IDataAccessLayer<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly object _lock = new object();

    public DataAccessLayer(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAsync(DataManagerRequest dm = default)
    {
        try
        {
            List<T> result = new List<T>();
            var query = _context.Set<T>().AsQueryable();

            object includesListAsString = string.Empty;
            bool hasIncludesKey = dm.Params?.TryGetValue(ParamsEnum.IncludesListSeparatedByComa.ToString(), out includesListAsString) ?? false;


            if (hasIncludesKey && !string.IsNullOrWhiteSpace(includesListAsString?.ToString()))
            {
                List<string> includesList = includesListAsString?.ToString().Split(",").ToList();
                foreach (var includeVal in includesList)
                {
                    query = query.Include(includeVal.ToString());
                }
            }

            if (dm != null)
            {
                query = await query.FilterBy(dm);
                query = await query.PageBy(dm);
            }

            result = await query.ToListAsync();
            //query = await query.GroupBy(dm);

            return result;

        }
        catch (Exception ex)
        {
            return Enumerable.Empty<T>();
        }

    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Set<T>().AsQueryable().CountAsync();
    }

    public async Task AddAsync(T obj)
    {
        await _context.Set<T>().AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T obj, string pkName)
    {
        if (await _context.Set<T>().FindAsync(obj.GetType().GetProperty(pkName).GetValue(obj)) is T found)
        {
            _context.Entry(found).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(object id)
    {
        if (await _context.Set<T>().FindAsync(id) is T found)
        {
            _context.Set<T>().Remove(found);
            await _context.SaveChangesAsync();
        }
    }

}