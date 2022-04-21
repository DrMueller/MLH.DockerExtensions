using Lamar;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DependencyInjection
{
    public static class TestContainerFactory
    {
        public static IContainer Create()
        {
            return new Container(
                cfg =>
                {
                    cfg.Scan(
                        scanner =>
                        {
                            scanner.AssembliesFromApplicationBaseDirectory();
                            scanner.LookForRegistries();
                        });
                });
        }
    }
}