using Docker.DotNet;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants
{
    public interface IDockerClientFactory
    {
        IDockerClient Create();
    }
}