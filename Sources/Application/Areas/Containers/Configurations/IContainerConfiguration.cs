using System.Collections.Generic;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations
{
    public interface IContainerConfiguration
    {
        string ContainerName { get; }
        IReadOnlyCollection<EnvironmentVariable> EnvironmentVariables { get; }
        ImageIdentifier ImageIdentifier { get; }
        PortConfiguration PortConfiguration { get; }
    }
}