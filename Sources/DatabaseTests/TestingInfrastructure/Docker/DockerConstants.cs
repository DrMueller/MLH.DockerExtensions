namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker
{
    // ATTENTION: These values are also used in the CI YAML.
    public static class DockerConstants
    {
        public const string ContainerName = "SQLServer2017";
        public const string HostIp = "localhost";
        public const string HostPort = "1437";
        public const string ImageName = "mcr.microsoft.com/mssql/server";
        public const string ImageTag = "2017-latest";
        public const string SaPassword = "sTronkpassword54322!";
        public static readonly string ConnectionString = $"Server={HostIp},{HostPort};Database=Master;User Id=SA;Password={SaPassword}";
    }
}