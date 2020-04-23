using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports
{
    public class ContainerPort
    {
        public ContainerPort(int portNumber, ContainerPortProcotol protocol)
        {
            Guard.That(() => portNumber > 0, "Port Number not set.");

            PortNumber = portNumber;
            Protocol = protocol;
        }

        public string CompleteIdentifier => $"{PortNumber}/{Protocol.Name}";

        public int PortNumber { get; }
        public ContainerPortProcotol Protocol { get; }
    }
}