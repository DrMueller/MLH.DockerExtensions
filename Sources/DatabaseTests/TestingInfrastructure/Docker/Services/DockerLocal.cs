using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services
{
    public static class DockerLocal
    {
        public const string ContainerName = "SQLServer2017";

        public static async Task InitializeAsync()
        {
            var client = new DockerClientConfiguration(
                    new Uri("npipe://./pipe/docker_engine"))
                .CreateClient();

            await client.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = "mcr.microsoft.com/mssql/server", Tag = "2017-latest" },
                new AuthConfig(),
                new Progress<JSONMessage>());

            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
            var existingContainer = containers.SingleOrDefault(f => f.Names.Contains("/" + ContainerName));
            
            if (existingContainer != null)
            {
                await client.Containers.StopContainerAsync(existingContainer.ID, new ContainerStopParameters
                {
                    WaitBeforeKillSeconds = 60
                });

                await client.Containers.RemoveContainerAsync(existingContainer.ID, new ContainerRemoveParameters
                {
                    Force = true
                });
            }

            var envVariables = new List<string>
            {
                "ACCEPT_EULA=Y",
                $"SA_PASSWORD={DockerInitializer.SaPassword}",
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
                                HostIP = "127.0.0.1",
                                HostPort = "1437"
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
                    Image = "mcr.microsoft.com/mssql/server:2017-latest",
                    Name = ContainerName,
                    Tty = false,
                    HostConfig = hostConfig,
                    Env = envVariables,
                    ExposedPorts = exposedPorts
                });

            await client.Containers.StartContainerAsync(container.ID, null);
        }
    }
}
