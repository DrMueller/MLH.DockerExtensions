using System.Threading.Tasks;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services
{
    public interface IContainerRemover
    {
        Task RemoveContainerAsync(string containerId);
    }
}