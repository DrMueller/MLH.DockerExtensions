using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.Services
{
    public interface IIndividualService
    {
        Task CreateAndLogIndividualsAsync(AppDbContext dbContext);
    }
}