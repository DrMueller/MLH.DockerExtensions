using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DbContexts;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DbContexts.Implementation;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Fixtures;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;
using Xunit;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingAreas
{
    public class DataBaseTests : DatabaseTestBase
    {
        private readonly AppDbContext _sut;

        public DataBaseTests(DatabaseTestFixture fixture)
        {
            _sut = fixture
                .LamarContainer
                .GetInstance<IDockerizedAppDbContextFactory>()
                .Create();
        }

        [Fact]
        public async Task Database_Works()
        {
            _sut.Individuals.Add(new IndividualDataModel());
            _sut.Individuals.Add(new IndividualDataModel());
            await _sut.SaveChangesAsync();

            var individualsCount =  await _sut.Individuals.CountAsync();
            Assert.Equal(2, individualsCount);
        }
    }
}
