using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.Factories;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.Factories.Implementation;
using StructureMap;

namespace Mmu.Mlh.DockerExtensions.FakeApp.Infrastructure.DependencyInjection
{
    public class FakeAppRegistry : Registry
    {
        public FakeAppRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<FakeAppRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IDbContextFactory>().Use<DbContextFactory>();
        }
    }
}