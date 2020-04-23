using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.KnownConfigurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;
using Mmu.Mlh.DockerExtensions.TestConsole.Areas.Services;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.ConsoleCommands
{
    public class UseDatabaseInExistingContainer : IConsoleCommand
    {
        private readonly IDockerizedContextFactory _dockerizedContextFactory;
        private readonly IIndividualService _individualService;

        public UseDatabaseInExistingContainer(
            IIndividualService individualService,
            IDockerizedContextFactory dockerizedContextFactory)
        {
            _individualService = individualService;
            _dockerizedContextFactory = dockerizedContextFactory;
        }

        public string Description { get; } = "Use Database in existing Container";
        public ConsoleKey Key { get; } = ConsoleKey.F3;

        public async Task ExecuteAsync()
        {
            var config = new SqlServer2017Latest(1337, "PersistentSqlServer");
            var context = _dockerizedContextFactory.Create(config);

            await context.ExecuteAsync(
                async container =>
                {
                    var dbContext = await CreateDbContextForContainerAsync(config);
                    await _individualService.CreateAndLogIndividualsAsync(dbContext);
                },
                false);
        }

        private static async Task<AppDbContext> CreateDbContextForContainerAsync(SqlServer2017Latest config)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(config.CreateConnectionString())
                .Options;

            var dbContext = new AppDbContext(options);
            await dbContext.Database.MigrateAsync();

            return dbContext;
        }
    }
}