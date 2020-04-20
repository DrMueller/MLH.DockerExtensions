using System.Collections.Generic;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services.Adapters
{
    internal interface IDockerApiAdapter
    {
        IList<string> AdaptEnvironmentVariables(IReadOnlyCollection<EnvironmentVariable> variables);
        IDictionary<string, EmptyStruct> AdaptExposedPorts(IReadOnlyCollection<Models.Configurations.Ports.PortBinding> ports);
        IDictionary<string, IList<PortBinding>> AdaptPortBindings(IReadOnlyCollection<Models.Configurations.Ports.PortBinding> ports);
    }
}