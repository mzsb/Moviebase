using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moviebase.DAL;

namespace Moviebase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(MoviebaseDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItemsAsync() =>
        await context.TestItems.ToListAsync();
    
}
