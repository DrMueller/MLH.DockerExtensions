using System.Collections.Generic;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants
{
    internal interface IDockerApiAdapter
    {
        IList<string> AdaptEnvironmentVariables(IReadOnlyCollection<EnvironmentVariable> variables);
        IDictionary<string, EmptyStruct> AdaptExposedPorts(IReadOnlyCollection<Configurations.Ports.PortBinding> ports);
        IDictionary<string, IList<PortBinding>> AdaptPortBindings(IReadOnlyCollection<Configurations.Ports.PortBinding> ports);
    }
}