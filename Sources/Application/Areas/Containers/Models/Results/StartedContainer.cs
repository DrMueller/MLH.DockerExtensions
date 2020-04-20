using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Results
{
    public class StartedContainer
    {
        public StartedContainer(ContainerPort usedPort)
        {
            Guard.ObjectNotNull(() => usedPort);
            UsedPort = usedPort;
        }

        public ContainerPort UsedPort { get; }
    }
}