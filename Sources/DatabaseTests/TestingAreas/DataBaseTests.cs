using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels;
using Xunit;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingAreas
{
    public class DataBaseTests
    {
        [Fact]
        public async Task Database_Works()
        {
            var dbContext = await DockerInitializer.InitializeAsync();
            dbContext.Individuals.Add(new IndividualDataModel());
            dbContext.Individuals.Add(new IndividualDataModel());
            await dbContext.SaveChangesAsync();

            var individualsCount =  await dbContext.Individuals.CountAsync();
            Assert.Equal(2, individualsCount);
        }
    }
}
