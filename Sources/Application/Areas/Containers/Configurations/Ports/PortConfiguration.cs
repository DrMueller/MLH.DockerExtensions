using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports
{
    public class PortConfiguration
    {
        public PortConfiguration(params PortBinding[] bindings)
        {
            Guard.ObjectNotNull(() => bindings);

            Bindings = bindings;
        }

        public IReadOnlyCollection<PortBinding> Bindings { get; }
    }
}