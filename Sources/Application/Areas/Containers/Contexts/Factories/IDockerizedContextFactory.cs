using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories
{
    public interface IDockerizedContextFactory
    {
        IDockerizedContext Create(IContainerConfiguration containerConfig);
    }
}