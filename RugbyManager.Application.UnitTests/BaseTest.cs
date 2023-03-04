using AutoMapper;
using RugbyManager.Application.Common.Mapping;
using RugbyManager.Application.UnitTests.DataPersistence;

namespace RugbyManager.Application.UnitTests;

public class BaseTest
{
    internal TestDbContext AppDbContext { get; set; } = new TestDbContext();
    internal IMapper mapper { get; set; }

    public BaseTest()
    {
        mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
            .CreateMapper();
    }
}