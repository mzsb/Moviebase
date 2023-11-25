#region Usings

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;

#endregion

namespace Moviebase.BLL.Services;

public class UserService(
    MoviebaseDbContext context,
    IMapper mapper) : IUserService
{
    public async Task<List<UserDto>> GetUsersAsync() =>
        await context.Users
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync();
}
