using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Results;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Eithers;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services
{
    public interface IContainerStarter
    {
        Task<Either<ContainerStartingError, StartedContainer>> StartContainerAsync(IContainerConfiguration containerConfig);
    }
}