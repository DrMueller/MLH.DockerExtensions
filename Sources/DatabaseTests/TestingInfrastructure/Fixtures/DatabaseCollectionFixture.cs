using Xunit;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Fixtures
{
    [CollectionDefinition(CollectionName)]
    public class DatabaseCollectionFixture : ICollectionFixture<DatabaseTestFixture>
    {
        public const string CollectionName = "Database";
    }
}