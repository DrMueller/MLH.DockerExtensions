using System.Collections.Generic;
using System.Linq;
using Docker.DotNet.Models;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Services.Servants.Implementation
{
    internal class DockerApiAdapter : IDockerApiAdapter
    {
        public IList<string> AdaptEnvironmentVariables(IReadOnlyCollection<EnvironmentVariable> variables)
        {
            return variables
                .Select(f => $"{f.Key}={f.Value}")
                .ToList();
        }

        public IDictionary<string, EmptyStruct> AdaptExposedPorts(IReadOnlyCollection<Configurations.Ports.PortBinding> ports)
        {
            return ports.ToDictionary(f => f.ContainerPort.CompleteIdentifier, f => default(EmptyStruct));
        }

        public IDictionary<string, IList<Docker.DotNet.Models.PortBinding>> AdaptPortBindings(IReadOnlyCollection<Configurations.Ports.PortBinding> ports)
        {
            return ports.ToDictionary(
                f => f.ContainerPort.CompleteIdentifier,
                f => AdaptPortBindings(f.HostPorts));
        }

        private static IList<Docker.DotNet.Models.PortBinding> AdaptPortBindings(IReadOnlyCollection<HostPort> hostPorts)
        {
            return hostPorts.Select(
                hp => new Docker.DotNet.Models.PortBinding
                {
                    HostIP = hp.HostIp,
                    HostPort = hp.PortNumber.ToString()
                }).ToList();
        }
    }
}