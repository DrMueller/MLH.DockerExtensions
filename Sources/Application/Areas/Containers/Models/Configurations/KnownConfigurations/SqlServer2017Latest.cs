using System.Collections.Generic;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.Ports;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations.KnownConfigurations
{
    public class SqlServer2017Latest : IContainerConfiguration
    {
        public const string SaPassword = "sTronkpassword54322!";
        public const int DefaultPort = 1433;

        public string ContainerNamePrefix { get; } = "SQLServer2017";

        public IReadOnlyCollection<EnvironmentVariable> EnvironmentVariables { get; } = new List<EnvironmentVariable>
        {
            new EnvironmentVariable("ACCEPT_EULA", "Y"),
            new EnvironmentVariable("SA_PASSWORD", SqlServer2017Latest.SaPassword),
            new EnvironmentVariable("MSSQL_PID", "Express")
        };

        public ImageIdentifier ImageIdentifier { get; } = new ImageIdentifier("mcr.microsoft.com/mssql/server", "2017-latest");

        public PortConfiguration PortConfiguration { get; } = new PortConfiguration(
            new List<PortBinding>
            {
                new PortBinding(
                    new ContainerPort(DefaultPort, ContainerPortProcotol.Tcp),
                    new List<HostPort>
                    {
                        new HostPort(DefaultPort, Maybe.CreateSome("127.0.0.1"))
                    })
            },
            DefaultPort);
    }
}