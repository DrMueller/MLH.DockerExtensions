using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports
{
    public class ContainerPortProcotol
    {
        private ContainerPortProcotol(string name)
        {
            Guard.StringNotNullOrEmpty(() => name);

            Name = name;
        }

        public static ContainerPortProcotol Tcp => new ContainerPortProcotol("tcp");
        public static ContainerPortProcotol Udp => new ContainerPortProcotol("udp");
        public string Name { get; }
    }
}