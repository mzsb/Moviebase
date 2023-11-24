#region Usings

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL.Migrations;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Services;

public class AccountService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService) : IAccountService
{
    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
        var users = await userManager.Users.ToListAsync();

        var user = await userManager.Users
            .SingleOrDefaultAsync(user => user.UserName == loginDto.Username);

        if (user is null) throw new AccountException("Invalid username");

        var result = await signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new AccountException("Invalid password");

        return new UserDto
        {
            Username = loginDto.Username,
            Token = await tokenService.CreateTokenAsync(user),
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User { UserName = registerDto.Username};
        await userManager.CreateAsync(user, registerDto.Password);
        await userManager.AddToRoleAsync(user, "User");

        return await LoginAsync(new LoginDto
        { 
            Username = registerDto.Username, 
            Password = registerDto.Password
        });
    }
}
