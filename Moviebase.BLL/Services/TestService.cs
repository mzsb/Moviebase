#region Usings

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;

#endregion

namespace Moviebase.BLL.Services;

public class TestService(MoviebaseDbContext context, IMapper mapper) : ITestService
{
    public async Task<List<TestItemDto>> GetTestItemsAsync() => await mapper
        .ProjectTo<TestItemDto>(context.TestItems)
        .ToListAsync();
}
