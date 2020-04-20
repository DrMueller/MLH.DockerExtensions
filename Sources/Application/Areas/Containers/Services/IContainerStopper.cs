using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Services
{
    public interface IContainerStopper
    {
        Task StopContainerAsync();
    }
}
