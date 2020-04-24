using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories.Implementation;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models.Implementation;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Implementation;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants.Implementation;
using StructureMap;

namespace Mmu.Mlh.DockerExtensions.Infrastructure.DependencyInjection
{
    public class DockerExtensionsRegistry : Registry
    {
        public DockerExtensionsRegistry()
        {
            For<IDockerizedContextFactory>().Use<DockerizedContextFactory>().Singleton();
            For<IContainerStarter>().Use<ContainerStarter>().Singleton();
            For<IContainerRemover>().Use<ContainerRemover>().Singleton();
            For<IDockerApiAdapter>().Use<DockerApiAdapter>().Singleton();
            For<IContainerFactory>().Use<ContainerFactory>().Singleton();
            For<IDockerContainerRepository>().Use<DockerContainerRepository>().Singleton();
            For<IDockerClientFactory>().Use<DockerClientFactory>().Singleton();

            For<DockerizedContext>().AlwaysUnique();
        }
    }
}