using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Eithers;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services
{
    public interface IContainerStarter
    {
        Task<Either<ContainerErrors, RunningContainer>> StartContainerAsync(string containerId);
    }
}