using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services
{
    public static class DockerInitializer
    {
        public const string SaPassword = "sTronkpassword54322!";

        public static async Task<AppDbContext> InitializeAsync()
        {
#if DEBUG
            await DockerLocal.InitializeAsync();
#endif

            var connectionString = $"Server=127.0.0.1,1437;Database=Master;User Id=SA;Password={SaPassword}";
            await WaitUntilDatabaseAvailableAsync(connectionString);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var dbContext = new AppDbContext(options);
            await dbContext.Database.MigrateAsync();

            return dbContext;
        }

        private static async Task WaitUntilDatabaseAvailableAsync(string connectionString)
        {
            var start = DateTime.UtcNow;
            const int maxWaitTimeSeconds = 60;
            var connectionEstablised = false;
            while (!connectionEstablised && start.AddSeconds(maxWaitTimeSeconds) > DateTime.UtcNow)
            {
                try
                {
                    using var sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();
                    connectionEstablised = true;
                }
                catch
                {
                    // If opening the SQL connection fails, SQL Server is not ready yet
                    await Task.Delay(500);
                }
            }

            if (!connectionEstablised)
            {
                throw new Exception("Connection to the SQL docker database could not be established within 60 seconds.");
            }
        }
    }
}