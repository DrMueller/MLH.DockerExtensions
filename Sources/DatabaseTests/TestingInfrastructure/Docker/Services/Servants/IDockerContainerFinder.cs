using System.Threading.Tasks;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.Docker.Services.Servants
{
    public interface IDockerContainerFinder
    {
        Task<FunctionResult<string>> TryFindingByNameAsync(string namepart);
    }
}