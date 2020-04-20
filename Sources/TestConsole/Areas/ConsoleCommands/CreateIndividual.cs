using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.Factories;

namespace Mmu.Mlh.DockerExtensions.TestConsole.Areas.ConsoleCommands
{
    public class CreateIndividual : IConsoleCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IDbContextFactory _dbContextFactory;

        public CreateIndividual(
            IDbContextFactory dbContextFactory,
            IConsoleWriter consoleWriter)
        {
            _dbContextFactory = dbContextFactory;
            _consoleWriter = consoleWriter;
        }

        public string Description { get; } = "Create Individual";
        public ConsoleKey Key { get; } = ConsoleKey.F1;

        public async Task ExecuteAsync()
        {
            await using var dbContext = await _dbContextFactory.CreateAsync();
            await dbContext.Individuals.AddAsync(
                new IndividualDataModel
                {
                    Birthdate = new DateTime(1986, 12, 29),
                    FirstName = "Matthias",
                    LastName = "Müller"
                });

            await dbContext.Individuals.AddAsync(
                new IndividualDataModel
                {
                    Birthdate = new DateTime(1964, 12, 18),
                    FirstName = "Steve",
                    LastName = "Austin"
                });

            await dbContext.SaveChangesAsync();

            var individuals = await dbContext.Individuals.ToListAsync();
            individuals.ForEach(
                ind =>
                {
                    _consoleWriter.WriteLine($"{ind.Id}, {ind.FirstName} {ind.LastName} {ind.Birthdate}");
                });
        }
    }
}