using RugbyManager.Application.UnitTests.DataPersistence;

namespace RugbyManager.Application.UnitTests;

public class BaseTest
{
    internal TestDbContext appContext { get; set; } = new TestDbContext();
}