using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports
{
    public class PortBinding
    {
        public PortBinding(ContainerPort containerPort, IReadOnlyCollection<HostPort> hostPorts)
        {
            Guard.ObjectNotNull(() => containerPort);
            Guard.ObjectNotNull(() => hostPorts);

            ContainerPort = containerPort;
            HostPorts = hostPorts;
        }

        public ContainerPort ContainerPort { get; }
        public IReadOnlyCollection<HostPort> HostPorts { get; }
    }
}