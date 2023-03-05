using AutoMapper;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.UnitTests.DataPersistence;

namespace RugbyManager.Application.UnitTests;

public class BaseTest
{
    internal TestDbContext _testDbContext { get; set; } = new TestDbContext();
    internal IMapper _mapper { get; set; }

    public BaseTest()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
            .CreateMapper();
    }
}