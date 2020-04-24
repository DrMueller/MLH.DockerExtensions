using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants.Implementation
{
    internal class DockerContainerRepository : IDockerContainerRepository
    {
        private readonly IDockerClientFactory _clientFactory;

        public DockerContainerRepository(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Maybe<ContainerListResponse>> FindByNameAsync(string containerName)
        {
            using (var client = _clientFactory.Create())
            {
                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
                var existingContainer = containers.SingleOrDefault(f => f.Names.Contains("/" + containerName));

                return Maybe.CreateFromNullable(existingContainer);
            }
        }
    }
}