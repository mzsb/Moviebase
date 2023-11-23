#region Usings

using AutoMapper;
using Moviebase.BLL.Dtos;
using Moviebase.DAL;

#endregion

namespace Moviebase.BLL.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<TestItem, TestItemDto>();
    }
}
