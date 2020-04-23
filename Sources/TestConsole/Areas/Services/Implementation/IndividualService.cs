using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.Services.Implementation
{
    public class IndividualService : IIndividualService
    {
        private readonly IConsoleWriter _consoleWriter;

        public IndividualService(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public async Task CreateAndLogIndividualsAsync(AppDbContext dbContext)
        {
            dbContext.Individuals.Add(
                new IndividualDataModel
                {
                    Birthdate = DateTime.Now,
                    FirstName = "Matthias " + DateTime.Now.Ticks,
                    LastName = "Müller " + DateTime.Now.Ticks
                });

            await dbContext.SaveChangesAsync();
            await LogAllIndividualsAsync(dbContext);
        }

        private async Task LogAllIndividualsAsync(AppDbContext dbContext)
        {
            var allIndividuals = await dbContext.Individuals.ToListAsync();
            allIndividuals.ForEach(ind => _consoleWriter.WriteLine($"{ind.Id}: {ind.FirstName} {ind.LastName}, {ind.Birthdate}"));
        }
    }
}