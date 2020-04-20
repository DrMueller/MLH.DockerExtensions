using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Servants;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Implementation
{
    internal class ContainerStopper : IContainerStopper
    {
        private readonly IDockerClientFactory _clientFactory;

        public ContainerStopper(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task StopContainerAsync(string containerId)
        {
            using (var client = _clientFactory.Create())
            {
                await client.Containers.KillContainerAsync(containerId, null);
            }
        }
    }
}