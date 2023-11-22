#region Usings

using Microsoft.EntityFrameworkCore;

#endregion

namespace Moviebase.DAL;

public class MoviebaseDbContext(DbContextOptions<MoviebaseDbContext> options) : DbContext(options)
{
    public DbSet<TestItem> TestItems { get; set; }
}
