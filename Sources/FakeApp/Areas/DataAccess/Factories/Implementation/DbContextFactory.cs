using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.KnownConfigurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services;

namespace Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.Factories.Implementation
{
    internal class DbContextFactory : IDbContextFactory
    {
        private readonly IContainerStarter _dockerContainerStarter;

        public DbContextFactory(IContainerStarter dockerContainerStarter)
        {
            _dockerContainerStarter = dockerContainerStarter;
        }

        public async Task<AppDbContext> CreateAsync()
        {
            var containerResult = await _dockerContainerStarter.StartContainerAsync(new SqlServer2017Latest());

            var startedContainer = containerResult.Reduce(f => throw new Exception(string.Join(',', f.ErrorMessages)));
            var connectionString = $"Server=127.0.0.1,{startedContainer.UsedPort};Database=Master;User Id=SA;Password={SqlServer2017Latest.SaPassword}";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new AppDbContext(options);
        }
    }
}
