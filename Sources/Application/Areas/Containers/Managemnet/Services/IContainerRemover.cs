using System.Threading.Tasks;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services
{
    public interface IContainerRemover
    {
        Task RemoveContainerAsync(string containerId);
    }
}