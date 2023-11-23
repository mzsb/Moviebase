#region Usings

using Moviebase.BLL.Dtos;

#endregion

namespace Moviebase.BLL.Interfaces;

public interface ITestService
{
    Task<List<TestItemDto>> GetTestItemsAsync();
}
