using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.Ports;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.KnownConfigurations
{
    public abstract class DatabaseContainerConfiguration : IContainerConfiguration
    {
        private const int ContainerPort = 1433;
        private const string SaPassword = "sTronkpassword54322!";
        private readonly int _hostPort;

        protected DatabaseContainerConfiguration(int hostPort, string containerName)
        {
            ContainerName = containerName;
            _hostPort = hostPort;
        }

        public string ContainerName { get; }

        public IReadOnlyCollection<EnvironmentVariable> EnvironmentVariables { get; } = new List<EnvironmentVariable>
        {
            new EnvironmentVariable("ACCEPT_EULA", "Y"),
            new EnvironmentVariable("SA_PASSWORD", SaPassword),
            new EnvironmentVariable("MSSQL_PID", "Express")
        };

        public abstract ImageIdentifier ImageIdentifier { get; }

        public PortConfiguration PortConfiguration => new PortConfiguration(
            new PortBinding(
                new ContainerPort(ContainerPort, ContainerPortProcotol.Tcp),
                new HostPort(_hostPort, "127.0.0.1")));

        public string CreateConnectionString()
        {
            var host = PortConfiguration.Bindings.Single().HostPorts.Single().CompleteHost;

            var connectionString = $"Server={host};Database=Master;User Id=SA;Password={SaPassword}";
            return connectionString;
        }
    }
}