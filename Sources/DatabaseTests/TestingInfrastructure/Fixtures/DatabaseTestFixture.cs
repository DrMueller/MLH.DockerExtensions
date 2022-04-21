using System.Threading.Tasks;
using JetBrains.Annotations;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DbContexts;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DependencyInjection;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services;
using Xunit;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Fixtures
{
    [UsedImplicitly]
    public class DatabaseTestFixture : IAsyncLifetime
    {
        private string _dockerContainerId;

        internal IContainer LamarContainer { get; private set; }

        public async Task DisposeAsync()
        {
            if (!string.IsNullOrEmpty(_dockerContainerId))
            {
                var dockerContainerRemover = LamarContainer.GetInstance<IContainerRemover>();
                await dockerContainerRemover.RemoveContainerAsync(_dockerContainerId);
            }

            LamarContainer?.Dispose();
        }

        public async Task InitializeAsync()
        {
            LamarContainer = TestContainerFactory.Create();

            await StartDockerContainerAsync();
            await WaitForDatabaseAsync();
            await MigrateDatabaseAsync();
        }

        private async Task MigrateDatabaseAsync()
        {
            var dbContxt = LamarContainer
                .GetInstance<IDockerizedAppDbContextFactory>()
                .Create();

            await dbContxt.Database.MigrateAsync();
        }

        private async Task StartDockerContainerAsync()
        {
            var dockerContainerStarter = LamarContainer.GetInstance<IContainerStarter>();
            _dockerContainerId = await dockerContainerStarter.StartContainerAsync();
        }

        private async Task WaitForDatabaseAsync()
        {
            var dockerContainerAwaiter = LamarContainer.GetInstance<IContainerAwaiter>();
            await dockerContainerAwaiter.WaitUntilDataseAvailableAsync();
        }
    }
}