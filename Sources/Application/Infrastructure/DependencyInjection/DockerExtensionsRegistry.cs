using Mmu.Mlh.DockerExtensions.Areas.Containers.Services;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Adapters;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Adapters.Implementation;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Implementation;
using StructureMap;

namespace Mmu.Mlh.DockerExtensions.Infrastructure.DependencyInjection
{
    public class DockerExtensionsRegistry : Registry
    {
        public DockerExtensionsRegistry()
        {
            For<IContainerStarter>().Use<ContainerStarter>().Singleton();
            For<IContainerStopper>().Use<ContainerStopper>().Singleton();
            For<IDockerApiAdapter>().Use<DockerApiAdapter>().Singleton();
        }
    }
}