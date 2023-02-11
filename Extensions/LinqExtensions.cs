using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Linq.Expressions;

namespace FinanceMyLife.Extensions;

public static class LinqExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }

    public static async Task<IQueryable<T>> FilterBy<T>(this IQueryable<T> query, DataManagerRequest dm)
    {
        if (dm.Search.IsNotNullOrEmpty())
        {
            query = DataOperations.PerformSearching(query, dm.Search);
        }
        if (dm.Sorted.IsNotNullOrEmpty()) //Sorting
        {
            query = DataOperations.PerformSorting(query, dm.Sorted);
        }
        if (dm.Where.IsNotNullOrEmpty()) //Filtering
        {
            query = DataOperations.PerformFiltering(query, dm.Where, dm.Where[0].Operator);
        }


        return query;
    }

    public static async Task<IQueryable<T>> PageBy<T>(this IQueryable<T> query, DataManagerRequest dm)
    {
        if (dm.Skip.IsNotNullOrEmpty())
        {
            query = DataOperations.PerformSkip(query, dm.Skip);   //Paging
        }
        if (dm.Take.IsNotNullOrEmpty())
        {
            query = DataOperations.PerformTake(query, dm.Take);
        }

        return query;
    }

    public static async Task<System.Collections.IEnumerable> GroupBy<Transaction>(this System.Collections.IEnumerable result, DataManagerRequest dm)
    {
        if (dm.Group != null)
        {
            foreach (var group in dm.Group)
            {
                result = DataUtil.Group<Transaction>(result, group, dm.Aggregates, 0, dm.GroupByFormatter);
            }
        }
        return result;


        //if (dm.Group != null && dm.Group.Any())
        //{
        //    var test = result;
        //    var res = DataOperations.PerformGrouping<T>(test, dm.Group);

        //}
        //return result;
    }

}
