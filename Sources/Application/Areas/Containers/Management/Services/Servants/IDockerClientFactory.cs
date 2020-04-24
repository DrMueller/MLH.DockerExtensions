using Docker.DotNet;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants
{
    internal interface IDockerClientFactory
    {
        DockerClient Create();
    }
}