////using System;
////using System.Collections.Generic;
////using System.Diagnostics;
////using System.Linq;
////using System.Threading;
////using System.Threading.Tasks;
////using Docker.DotNet;
////using Docker.DotNet.Models;
////using Mmu.Mlh.TestingExtensions.Areas.Dockerization.Models;

////namespace Mmu.Mlh.TestingExtensions.Areas.Dockerization.Services.Implementation
////{
////    internal class DockerContainerStarter : IDockerContainerStarter
////    {
////        private const string ConnectionStringTemplate = @"Server=127.0.0.1,{0};Database=Master;User Id=SA;Password={1}";
////        private const string ContainerNamePrefix = "TestContainer";
////        private const string ContainerPort = "1433/tcp";
////        private const string ImageName = "mcr.microsoft.com/mssql/server";
////        private const string ImageTag = "2017-latest";
////        private const string SaPassword = "12345!54321HelloSa";
////        private const ushort StartingHostPort = 1433;

////        public async Task<SqlServerInDockerContainer> StartSqlContainerAsync()
////        {
////            using (var conf = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")))
////            using (var client = conf.CreateClient())
////            {
////                var sw = Stopwatch.StartNew();
////                Debug.WriteLine("Docker: starting to create image..");
////                await CreateImageAsync(client);
////                Debug.WriteLine($"Docker {sw.Elapsed:mm\\:ss}: image created..");

////                var hostPort = await GetHostPortAsync(client);
////                var containerId = await CreateContainerAsync(client, hostPort);
////                Debug.WriteLine($"Docker {sw.Elapsed:mm\\:ss}: container created..");

////                await StartContainerAsync(client, containerId);
////                Debug.WriteLine($"Docker {sw.Elapsed:mm\\:ss}: container created..");

////                var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
////                var currentContainer = containers.Single(f => f.ID == containerId);

////                Debug.WriteLine($"Docker state: {currentContainer.State}");
////                Debug.WriteLine($"Docker status: {currentContainer.Status}");

////                Thread.Sleep(6000);

////                var connectionString = string.Format(ConnectionStringTemplate, hostPort, SaPassword);

////                return new SqlServerInDockerContainer(connectionString);
////            }
////        }

////        private static async Task<string> CreateContainerAsync(IDockerClient client, uint hostPort)
////        {
////            var hostConfig = CreateHostConfig(client, hostPort);

////            var containerResult = await client.Containers.CreateContainerAsync(
////                new CreateContainerParameters
////                {
////                    Image = ImageName + ":" + ImageTag,
////                    Name = ContainerNamePrefix + Guid.NewGuid(),
////                    Tty = false,
////                    HostConfig = hostConfig,
////                    Env = new[]
////                    {
////                        "ACCEPT_EULA=Y",
////                        $"SA_PASSWORD={SaPassword}",
////                        "MSSQL_PID=Express"
////                    },
////                    ExposedPorts = new Dictionary<string, EmptyStruct>
////                    {
////                        { ContainerPort, new EmptyStruct() }
////                    }
////                });

////            return containerResult.ID;
////        }

////        private static HostConfig CreateHostConfig(IDockerClient client, uint hostPort)
////        {
////            return new HostConfig
////            {
////                PortBindings = new Dictionary<string, IList<PortBinding>>
////                {
////                    {
////                        ContainerPort, new List<PortBinding>
////                        {
////                            new PortBinding { HostIP = "127.0.0.1", HostPort = hostPort.ToString() }
////                        }
////                    }
////                }
////            };
////        }

////        private static async Task CreateImageAsync(IDockerClient client)
////        {
////            await client.Images.CreateImageAsync(
////                new ImagesCreateParameters { FromImage = ImageName, Tag = ImageTag },
////                new AuthConfig(),
////                new Progress<JSONMessage>());
////        }

////        private static async Task<uint> GetHostPortAsync(IDockerClient client)
////        {
////            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });

////            var lastUsedPort = containers
////                .Where(f => f.Names.Any(name => name.StartsWith($"/{ContainerNamePrefix}")))
////                .SelectMany(f => f.Ports)
////                .OrderByDescending(f => f.PublicPort)
////                .FirstOrDefault()?.PublicPort;

////            if (lastUsedPort.HasValue)
////            {
////                return (uint)lastUsedPort.Value + 1;
////            }

////            return StartingHostPort;
////        }

////        private static async Task StartContainerAsync(IDockerClient client, string containerId)
////        {
////            await client.Containers.StartContainerAsync(containerId, null);
////        }
////    }
////}