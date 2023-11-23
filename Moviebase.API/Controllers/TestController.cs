using Microsoft.AspNetCore.Mvc;
using Moviebase.BLL.Dtos;
using Moviebase.BLL.Interfaces;

namespace Moviebase.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(ITestService testService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestItemDto>>> GetTestItemsAsync() =>
        await testService.GetTestItemsAsync();
}
