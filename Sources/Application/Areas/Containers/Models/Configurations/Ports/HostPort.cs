using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports
{
    public class HostPort
    {
        public HostPort(int portNumber, Maybe<string> hostIp)
        {
            Guard.That(() => portNumber > 0, "Port number not set.");
            Guard.ObjectNotNull(() => hostIp);

            PortNumber = portNumber;
            HostIp = hostIp;
        }

        public Maybe<string> HostIp { get; }
        public int PortNumber { get; }
    }
}