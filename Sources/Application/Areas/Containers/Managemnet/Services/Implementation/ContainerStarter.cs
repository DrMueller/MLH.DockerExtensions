using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services.Servants;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Eithers;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services.Implementation
{
    internal class ContainerStarter : IContainerStarter
    {
        private readonly IDockerClientFactory _clientFactory;

        public ContainerStarter(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Either<ContainerErrors, RunningContainer>> StartContainerAsync(string containerId)
        {
            using (var client = _clientFactory.Create())
            {
                // Strange interface for the filtering, see https://github.com/dotnet/Docker.DotNet/issues/303
                var dict = new Dictionary<string, IDictionary<string, bool>>
                {
                    {
                        "id",
                        new Dictionary<string, bool>
                        {
                            { containerId, true }
                        }
                    }
                };

                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { Filters = dict });
                var container = containers.SingleOrDefault();

                if (container?.State.ToLower() == "running")
                {
                    return new RunningContainer(container.ID);
                }

                var containerStarted = await client.Containers.StartContainerAsync(containerId, null);
                if (!containerStarted)
                {
                    return new ContainerErrors("Could not start Container.");
                }

                return new RunningContainer(containerId);
            }
        }
    }
}