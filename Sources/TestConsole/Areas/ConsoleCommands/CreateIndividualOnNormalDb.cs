using System;
using System.Threading.Tasks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;
using Mmu.Mlh.DockerExtensions.TestConsole.Areas.Services;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.ConsoleCommands
{
    public class CreateIndividualOnNormalDb : IConsoleCommand
    {
        private readonly IIndividualService _individualService;

        public CreateIndividualOnNormalDb(IIndividualService individualService)
        {
            _individualService = individualService;
        }

        public string Description { get; } = "Create Individual on normal DB";
        public ConsoleKey Key { get; } = ConsoleKey.F1;

        public async Task ExecuteAsync()
        {
            var dbContext = new AppDbContext();
            await _individualService.CreateAndLogIndividualsAsync(dbContext);
        }
    }
}