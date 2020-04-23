using System;
using Docker.DotNet;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services.Servants.Implementation
{
    internal class DockerClientFactory : IDockerClientFactory
    {
        public DockerClient Create()
        {
            var clientConfig = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine"));
            return clientConfig.CreateClient();
        }
    }
}