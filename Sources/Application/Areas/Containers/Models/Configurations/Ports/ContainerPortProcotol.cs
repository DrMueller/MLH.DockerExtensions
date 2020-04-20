namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports
{
    public class ContainerPortProcotol
    {
        private ContainerPortProcotol(string name)
        {
            Name = name;
        }

        public static ContainerPortProcotol Tcp => new ContainerPortProcotol("tcp");
        public static ContainerPortProcotol Udp => new ContainerPortProcotol("udp");
        public string Name { get; }
    }
}