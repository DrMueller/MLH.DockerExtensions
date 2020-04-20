using System.Threading.Tasks;

namespace Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.Factories
{
    public interface IDbContextFactory
    {
        Task<AppDbContext> CreateAsync();
    }
}