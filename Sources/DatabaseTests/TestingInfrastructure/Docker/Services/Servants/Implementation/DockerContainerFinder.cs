using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using JetBrains.Annotations;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants.Implementation
{
    [UsedImplicitly]
    public class DockerContainerFinder : IDockerContainerFinder
    {
        private readonly IDockerClientFactory _clientFactory;

        public DockerContainerFinder(IDockerClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Maybe<string>> TryFindingIdByNameAsync(string namepart)
        {
            using var client = _clientFactory.Create();

            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
            var existingContainer = containers.SingleOrDefault(f => f.Names.Contains("/" + namepart));

            return existingContainer == null
                ? Maybe.CreateNone<string>()
                : existingContainer.ID;
        }
    }
}