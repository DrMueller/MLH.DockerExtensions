using System.Threading.Tasks;
using Docker.DotNet.Models;
using JetBrains.Annotations;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Implementation
{
    [UsedImplicitly]
    public class ContainerRemover : IContainerRemover
    {
        private readonly IDockerClientFactory _clientFactory;

        public ContainerRemover(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task RemoveContainerAsync(string containerId)
        {
            using var client = _clientFactory.Create();

            await client.Containers.KillContainerAsync(containerId, new ContainerKillParameters());
            await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters { Force = true });
        }
    }
}