namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.KnownConfigurations
{
    public class SqlServer2022Latest : DatabaseContainerConfiguration
    {
        public SqlServer2022Latest(int hostPort = 1437, string containerName = "SQLServer2022") : base(hostPort, containerName)
        {
        }

        public override ImageIdentifier ImageIdentifier { get; } = new ImageIdentifier("mcr.microsoft.com/mssql/server", "2022-latest");
    }
}