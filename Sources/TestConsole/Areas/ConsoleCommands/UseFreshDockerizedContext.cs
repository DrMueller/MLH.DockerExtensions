using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations.KnownConfigurations;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Factories;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.ConsoleCommands
{
    public class UseFreshDockerizedContext : IConsoleCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IDockerizedContextFactory _dockerizedContextFactory;

        public UseFreshDockerizedContext(
            IConsoleWriter consoleWriter,
            IDockerizedContextFactory dockerizedContextFactory)
        {
            _consoleWriter = consoleWriter;
            _dockerizedContextFactory = dockerizedContextFactory;
        }

        public string Description { get; } = "Use fresh dockerized context";
        public ConsoleKey Key { get; } = ConsoleKey.F2;

        public async Task ExecuteAsync()
        {
            var config = new SqlServer2017Latest();
            var connectionString =config.CreateConnectionString();
            _consoleWriter.WriteLine(connectionString);

            var context = _dockerizedContextFactory.Create(config);
            
            await context.ExecuteAsync(
                container =>
                {
                    _consoleWriter.WriteLine("Container ID: " + container.ContainerId);
                    return Task.CompletedTask;
                },
                true);
        }
    }
}