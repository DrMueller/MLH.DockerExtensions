using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Eithers;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services
{
    public interface IContainerFactory
    {
        Task<Either<ContainerErrors, CreatedContainer>> CreateIfNotExistingAsync(IContainerConfiguration containerConfig);
    }
}