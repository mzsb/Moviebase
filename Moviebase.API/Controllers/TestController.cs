using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moviebase.DAL;

namespace Moviebase.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly MoviebaseDbContext _context;

    public TestController(MoviebaseDbContext context) => _context = context; 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItemsAsync() =>
        await _context.TestItems.ToListAsync();
    
}
