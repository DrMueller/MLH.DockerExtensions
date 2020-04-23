using System;
using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models.Implementation
{
    internal class DockerizedContext : IDockerizedContext
    {
        private IContainerConfiguration _containerConfig;
        private readonly IContainerFactory _containerFactory;
        private readonly IContainerStarter _containerStarter;
        private readonly IContainerRemover _containerStopper;

        public DockerizedContext(
            IContainerFactory containerFactory,
            IContainerStarter containerStarter,
            IContainerRemover containerStopper)
        {
            _containerFactory = containerFactory;
            _containerStarter = containerStarter;
            _containerStopper = containerStopper;
        }

        public async Task ExecuteAsync(Func<RunningContainer, Task> callback, bool removeContainerAfter)
        {
            var containerId = string.Empty;

            try
            {
                containerId = await InitializeContainerAsync();
                var runningContainer = new RunningContainer(containerId);

                await callback(runningContainer);
            }
            finally
            {
                if (removeContainerAfter)
                {
                    await _containerStopper.RemoveContainerAsync(containerId);
                }
            }
        }

        internal void Initialize(IContainerConfiguration containerConfig)
        {
            _containerConfig = containerConfig;
        }

        private async Task<string> InitializeContainerAsync()
        {
            var creationResult = await _containerFactory.CreateIfNotExistingAsync(_containerConfig);
            var createdContainer = creationResult.Reduce(errors => throw new Exception(string.Join(", ", errors)));

            var startResult = await _containerStarter.StartContainerAsync(createdContainer.Id);
            var startedContainer = startResult.Reduce(errors => throw new Exception(string.Join(", ", errors)));

            return startedContainer.ContainerId;
        }
    }
}