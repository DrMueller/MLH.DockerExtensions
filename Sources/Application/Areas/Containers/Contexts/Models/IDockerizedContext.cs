﻿using System;
using System.Threading.Tasks;
using Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Contexts.Models
{
    public interface IDockerizedContext
    {
        Task ExecuteAsync(Func<RunningContainer, Task> callback, bool removeContainerAfter);
    }
}