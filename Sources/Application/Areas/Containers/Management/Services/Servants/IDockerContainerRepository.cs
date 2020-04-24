using System.Threading.Tasks;
using Docker.DotNet.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants
{
    internal interface IDockerContainerRepository
    {
        Task<Maybe<ContainerListResponse>> FindByNameAsync(string containerName);
    }
}