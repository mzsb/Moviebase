namespace Moviebase.DAL;

public static class TestSeeder
{
    public static async Task SeedTestItemsAsync(this MoviebaseDbContext context)
    {
        await context.TestItems.AddRangeAsync(Enumerable.Range(0, 10)
            .Select(_ => new TestItem()));

        await context.SaveChangesAsync();
    }
}
