using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models
{
    public class RunningContainer
    {
        public string ContainerId { get; }

        public RunningContainer(string containerId)
        {
            Guard.StringNotNullOrEmpty(() => containerId);

            ContainerId = containerId;
        }
    }
}