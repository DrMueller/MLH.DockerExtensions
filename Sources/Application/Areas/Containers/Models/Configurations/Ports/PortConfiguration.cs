using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports
{
    public class PortConfiguration
    {
        public PortConfiguration(IReadOnlyCollection<PortBinding> bindings, int startingPort)
        {
            Guard.ObjectNotNull(() => bindings);
            Guard.That(() => startingPort > 0, $"{nameof(startingPort)} not set.");

            Bindings = bindings;
            StartingPort = startingPort;
        }

        public IReadOnlyCollection<PortBinding> Bindings { get; }
        public int StartingPort { get; }
    }
}