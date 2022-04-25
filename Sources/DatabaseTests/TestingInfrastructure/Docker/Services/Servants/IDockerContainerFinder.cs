using System.Threading.Tasks;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants
{
    public interface IDockerContainerFinder
    {
        Task<Maybe<string>> TryFindingIdByNameAsync(string namepart);
    }
}