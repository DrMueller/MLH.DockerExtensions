using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models.Implementation;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories.Implementation
{
    internal class DockerizedContextFactory : IDockerizedContextFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public DockerizedContextFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IDockerizedContext Create(IContainerConfiguration containerConfig)
        {
            var context = _serviceLocator.GetService<DockerizedContext>();
            context.Initialize(containerConfig);

            return context;
        }
    }
}