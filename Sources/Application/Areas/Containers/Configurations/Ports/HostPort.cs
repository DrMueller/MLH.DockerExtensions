using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports
{
    public class HostPort
    {
        public const string DefaultHostIp = "127.0.0.1";

        public HostPort(int portNumber, Maybe<string> possibleIp)
        {
            Guard.That(() => portNumber > 0, "Port number not set.");
            Guard.ObjectNotNull(() => possibleIp);

            PortNumber = portNumber;
            HostIp = possibleIp.Reduce(() => DefaultHostIp);
        }

        public string CompleteHost => $"{HostIp},{PortNumber}";

        public string HostIp { get; }
        public int PortNumber { get; }
    }
}