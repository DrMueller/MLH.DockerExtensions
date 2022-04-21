using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.SqlClient;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Implementation
{
    [UsedImplicitly]
    public class ContainerAwaiter : IContainerAwaiter
    {
        public async Task WaitUntilDataseAvailableAsync()
        {
            var start = DateTime.UtcNow;
            const int MaxWaitTimeSeconds = 60;
            var connectionEstablised = false;

            while (!connectionEstablised && start.AddSeconds(MaxWaitTimeSeconds) > DateTime.UtcNow)
            {
                try
                {
                    await using var sqlConnection = new SqlConnection(DockerConstants.ConnectionString);
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