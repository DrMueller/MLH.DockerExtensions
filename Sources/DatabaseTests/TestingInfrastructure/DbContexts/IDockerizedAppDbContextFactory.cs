﻿using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts;

namespace Mmu.Mlh.DockerExtensions.DatabaseTests.TestingInfrastructure.DbContexts
{
    public interface IDockerizedAppDbContextFactory
    {
        AppDbContext Create();
    }
}