using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Results;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Servants;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Eithers;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Implementation
{
    internal class ContainerStarter : IContainerStarter
    {
        private readonly IDockerClientFactory _clientFactory;
        private readonly IDockerApiAdapter _dockerApiAdapter;

        public ContainerStarter(
            IDockerClientFactory clientFactory,
            IDockerApiAdapter dockerApiAdapter)
        {
            _clientFactory = clientFactory;
            _dockerApiAdapter = dockerApiAdapter;
        }

        public async Task<Either<ContainerStartingError, StartedContainer>> StartContainerAsync(IContainerConfiguration containerConfig)
        {
            using (var client = _clientFactory.Create())
            {
                await CreateImageAsync(client, containerConfig);
                var createContainerResponse = await CreateContainerAsync(client, containerConfig);
                if (createContainerResponse.Warnings.Any())
                {
                    return new ContainerStartingError(createContainerResponse.Warnings.ToList());
                }
                
                await StartContainerAsync(client, createContainerResponse.ID);

                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
                var currentContainer = containers.Single(f => f.ID == createContainerResponse.ID);

                Thread.Sleep(6000);
            }

            return null;
        }

        private static async Task CreateImageAsync(IDockerClient client, IContainerConfiguration image)
        {
            await client.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = image.ImageIdentifier.Name, Tag = image.ImageIdentifier.Tag },
                new AuthConfig(),
                new Progress<JSONMessage>());
        }

        private static async Task<int> GetHostPortAsync(IDockerClient client, IContainerConfiguration containerConfig)
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });

            var lastUsedPort = containers
                .Where(f => f.Names.Any(name => name.StartsWith($"/{containerConfig.ContainerNamePrefix}", StringComparison.Ordinal)))
                .SelectMany(f => f.Ports)
                .OrderByDescending(f => f.PublicPort)
                .FirstOrDefault()?.PublicPort;

            if (lastUsedPort.HasValue)
            {
                return lastUsedPort.Value + 1;
            }

            return containerConfig.PortConfiguration.StartingPort;
        }

        private static async Task<bool> StartContainerAsync(IDockerClient client, string containerId)
        {
            return await client.Containers.StartContainerAsync(containerId, null);
        }

        private async Task<CreateContainerResponse> CreateContainerAsync(
            IDockerClient client,
            IContainerConfiguration containerConfig)
        {
            var hostConfig = CreateHostConfig(containerConfig);
            return await client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = containerConfig.ImageIdentifier.CompleteIdentifier,
                    Name = containerConfig.ContainerNamePrefix + Guid.NewGuid(),
                    Tty = false,
                    HostConfig = hostConfig,
                    Env = _dockerApiAdapter.AdaptEnvironmentVariables(containerConfig.EnvironmentVariables),
                    ExposedPorts = _dockerApiAdapter.AdaptExposedPorts(containerConfig.PortConfiguration.Bindings)
                });
        }

        private HostConfig CreateHostConfig(IContainerConfiguration containerConfig)
        {
            return new HostConfig
            {
                PortBindings = _dockerApiAdapter.AdaptPortBindings(containerConfig.PortConfiguration.Bindings)
            };
        }
    }
}