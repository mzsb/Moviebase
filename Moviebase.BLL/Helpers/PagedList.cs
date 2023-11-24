#region Usings

using Microsoft.EntityFrameworkCore;

#endregion

namespace Moviebase.BLL.Helpers;

public class PagedList<T> : List<T>
{
    private PagedList(IEnumerable<T> items, int totalCount)
    {
        TotalCount = totalCount;
        AddRange(items);
    }

    public int TotalCount { get; set; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return new PagedList<T>(items, totalCount);
    }
}
