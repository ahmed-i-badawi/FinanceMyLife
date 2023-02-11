using FinanceMyLife.Data.Interfaces;
using FinanceMyLife.Extensions;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace FinanceMyLife.Data.Services;

public class DataAccessLayer<T> : IDataAccessLayer<T> where T : class
{
    private readonly ApplicationDbContext _context;

    public DataAccessLayer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAsync(DataManagerRequest dm = default)
    {
        var query = _context.Set<T>().AsQueryable();

        if (dm != null)
        {
            query = await query.FilterBy(dm);
            query = await query.PageBy(dm);
        }

        return await query.ToListAsync();
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