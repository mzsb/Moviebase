#region Usings

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Exceptions;
using Moviebase.BLL.Interfaces;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.BLL.Services;

public class AccountService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenService tokenService,
    IMapper mapper) : IAccountService
{
    public async Task<LoggedDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username) 
            ?? throw new AccountException("Invalid username");

        var result = await signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new AccountException("Invalid password");

        return new LoggedDto
        {
            User = mapper.Map<UserDto>(user),
            Token = await tokenService.CreateTokenAsync(user)
        };
    }

    public async Task<LoggedDto> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User { UserName = registerDto.Username };

        try
        {
            await userManager.CreateAsync(user, registerDto.Password);
            await userManager.AddToRoleAsync(user, "User");
        }
        catch (InvalidOperationException) { throw new AccountException("Registration failed"); }

        return await LoginAsync(new LoginDto
        {
            Username = user.UserName, 
            Password = registerDto.Password
        });
    }
}
