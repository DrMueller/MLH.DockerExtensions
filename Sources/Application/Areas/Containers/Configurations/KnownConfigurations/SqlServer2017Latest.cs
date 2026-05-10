namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.KnownConfigurations
{
    public class SqlServer2017Latest : DatabaseContainerConfiguration
    {
        public SqlServer2017Latest(int hostPort = 1437, string containerName = "SQLServer2017") : base(hostPort, containerName)
        {
        }

        public override ImageIdentifier ImageIdentifier { get; } = new ImageIdentifier("mcr.microsoft.com/mssql/server", "2017-latest");
    }
}