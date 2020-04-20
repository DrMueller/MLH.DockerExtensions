using Docker.DotNet;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Servants
{
    internal interface IDockerClientFactory
    {
        DockerClient Create();
    }
}