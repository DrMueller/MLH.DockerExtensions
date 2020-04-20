using System.Collections.Generic;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations
{
    public interface IContainerConfiguration
    {
        string ContainerNamePrefix { get; }
        IReadOnlyCollection<EnvironmentVariable> EnvironmentVariables { get; }
        ImageIdentifier ImageIdentifier { get; }
        PortConfiguration PortConfiguration { get; }
    }
}