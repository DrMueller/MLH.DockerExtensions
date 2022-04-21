using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using JetBrains.Annotations;
using Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Implementation
{
    [UsedImplicitly]
    public class ContainerStarter : IContainerStarter
    {
        private readonly IDockerClientFactory _clientFactory;
        private readonly IDockerContainerFinder _containerFinder;

        public ContainerStarter(
            IDockerContainerFinder containerFinder,
            IDockerClientFactory clientFactory)
        {
            _containerFinder = containerFinder;
            _clientFactory = clientFactory;
        }

        public async Task<string> StartContainerAsync()
        {
            var existingContainerResult = await _containerFinder.TryFindingIdByNameAsync(DockerConstants.ContainerName);

            return existingContainerResult.Reduce(SetupContainerAsync);
        }

        private async Task<string> SetupContainerAsync()
        {
            using var client = _clientFactory.Create();
            await CreateImageAsync(client);
            await RemoveContainerIfExistingAsync(client);
            var containerId = await CreateContainerAsync(client);

            await client.Containers.StartContainerAsync(containerId, null);

            return containerId;
        }

        private static async Task<string> CreateContainerAsync(IDockerClient client)
        {
            var envVariables = new List<string>
            {
                "ACCEPT_EULA=Y",
                $"SA_PASSWORD={DockerConstants.SaPassword}",
                "MSSQL_PID=Express",
            };

            var hostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        "1433/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostIP = DockerConstants.HostIp,
                                HostPort = DockerConstants.HostPort
                            }
                        }
                    }
                }
            };

            var exposedPorts = new Dictionary<string, EmptyStruct>
            {
                { "1433/tcp", new EmptyStruct() }
            };

            var container = await client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = $"{DockerConstants.ImageName}:{DockerConstants.ImageTag}",
                    Name = DockerConstants.ContainerName,
                    Tty = false,
                    HostConfig = hostConfig,
                    Env = envVariables,
                    ExposedPorts = exposedPorts
                });

            return container.ID;
        }

        private static async Task CreateImageAsync(IDockerClient client)
        {
            await client.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = DockerConstants.ImageName, Tag = DockerConstants.ImageTag },
                new AuthConfig(),
                new Progress<JSONMessage>());
        }

        private static async Task RemoveContainerIfExistingAsync(IDockerClient client)
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
            var existingContainer = containers.SingleOrDefault(f => f.Names.Contains("/" + DockerConstants.ContainerName));

            if (existingContainer != null)
            {
                await client.Containers.StopContainerAsync(
                    existingContainer.ID,
                    new ContainerStopParameters
                    {
                        WaitBeforeKillSeconds = 60
                    });

                await client.Containers.RemoveContainerAsync(
                    existingContainer.ID,
                    new ContainerRemoveParameters
                    {
                        Force = true
                    });
            }
        }
    }
}