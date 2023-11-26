#region Usings

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Services;

public class UserService(
    UserManager<User> userManager,
    IMapper mapper) : IUserService
{
    public async Task<List<UserDto>> GetUsersAsync() =>
        await userManager.Users
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    public async Task DeleteUserAsync(Guid userId) => await userManager.Users
        .Where(user => user.Id == userId)
        .ExecuteDeleteAsync();
}
