using System.Threading.Tasks;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services
{
    public interface IContainerRemover
    {
        Task RemoveContainerAsync(string containerId);
    }
}