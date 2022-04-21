using System;
using Docker.DotNet;
using JetBrains.Annotations;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants.Implementation
{
    [UsedImplicitly]
    public class DockerClientFactory : IDockerClientFactory
    {
        private readonly Lazy<DockerClient> _lazyClient;

        public DockerClientFactory()
        {
            _lazyClient = new Lazy<DockerClient>(CreateClient);
        }

        public IDockerClient Create()
        {
            return _lazyClient.Value;
        }

        private static DockerClient CreateClient()
        {
            var isRunningOnWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

            var dockerUri = isRunningOnWindows
                ? "npipe://./pipe/docker_engine"
                : "unix:///var/run/docker.sock";

            return new DockerClientConfiguration(new Uri(dockerUri))
                .CreateClient();
        }
    }
}