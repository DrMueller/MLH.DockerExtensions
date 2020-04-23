using Docker.DotNet;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Services.Servants
{
    internal interface IDockerClientFactory
    {
        DockerClient Create();
    }
}