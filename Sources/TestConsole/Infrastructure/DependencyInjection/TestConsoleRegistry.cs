using Lamar;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Infrastructure.DependencyInjection
{
    public class TestConsoleRegistry : ServiceRegistry
    {
        public TestConsoleRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestConsoleRegistry>();
                    scanner.AddAllTypesOf<IConsoleCommand>();
                    scanner.WithDefaultConventions();
                });
        }
    }
}