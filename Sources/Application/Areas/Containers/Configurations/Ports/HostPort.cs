using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports
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

        public string CompleteHost
        {
            get
            {
                var ip = HostIp.Reduce(() => "127.0.0.1");
                return $"{ip},{PortNumber}";
            }
        }

        public Maybe<string> HostIp { get; }
        public int PortNumber { get; }
    }
}