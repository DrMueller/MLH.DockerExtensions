using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services
{
    public static class DockerInitializer
    {
        private const string SaPassword = "sTronkpassword54322!";

        public static async Task<AppDbContext> InitializeAsync()
        {
            var client = new DockerClientConfiguration(
                    new Uri("npipe://./pipe/docker_engine"))
                .CreateClient();

            await client.Images.CreateImageAsync(
                new ImagesCreateParameters { FromImage = "mcr.microsoft.com/mssql/server", Tag = "2017-latest" },
                new AuthConfig(),
                new Progress<JSONMessage>());

            var envVariables = new List<string>
            {
                "ACCEPT_EULA=Y",
                $"SA_PASSWORD={SaPassword}",
                "MSSQL_PID=Express",
            };

            var hostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        "1433/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostIP = "127.0.0.1",
                                HostPort = "1437"
                            }
                        }
                    }
                }
            };

            var exposedPorts = new Dictionary<string, EmptyStruct>
            {
                { "1433/tcp", new EmptyStruct() }
            };

            var container = await client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = "mcr.microsoft.com/mssql/server:2017-latest",
                    Name = "SQLServer2017" +  Guid.NewGuid().ToString().Replace("-", string.Empty),
                    Tty = false,
                    HostConfig = hostConfig,
                    Env = envVariables,
                    ExposedPorts = exposedPorts
                });

            await client.Containers.StartContainerAsync(container.ID, null);

            var connectionString = $"Server=127.0.0.1,1437;Database=Master;User Id=SA;Password={SaPassword}"; 
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var dbContext = new AppDbContext(options);

            var tra = await dbContext.Database.ExecuteSqlRawAsync("SELECT * from [dbo].[spt_monitor]");
            
            await dbContext.Database.MigrateAsync();

            return dbContext;
        }

    }
}
