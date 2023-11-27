#region Usings

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL;
using Moviebase.DAL.Model;
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
    public async Task DeleteUserAsync(Guid userId)
    {
        if (await userManager.Users
        .Where(user => user.Id == userId)
        .ExecuteDeleteAsync() < 1)
            throw new UserException("User deletion failed");
    }
}
