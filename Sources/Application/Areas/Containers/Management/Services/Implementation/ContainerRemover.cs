using System.Threading.Tasks;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Implementation
{
    internal class ContainerRemover : IContainerRemover
    {
        private readonly IDockerClientFactory _clientFactory;

        public ContainerRemover(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task RemoveContainerAsync(string containerId)
        {
            if (string.IsNullOrEmpty(containerId))
            {
                return;
            }

            using (var client = _clientFactory.Create())
            {
                await client.Containers.KillContainerAsync(containerId, new ContainerKillParameters());
                await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters { Force = true });
            }
        }
    }
}