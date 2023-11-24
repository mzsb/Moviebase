#region Usings

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moviebase.DAL;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(MoviebaseDbContext context)
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync() =>
        await context.Users.ToListAsync();
}
