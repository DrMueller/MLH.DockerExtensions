using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DbContexts.Implementation
{
    [UsedImplicitly]
    public class DockerizedAppDbContextFactory : IDockerizedAppDbContextFactory
    {
        public AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(DockerConstants.ConnectionString)
                .Options;

            return new AppDbContext(options);
        }
    }
}