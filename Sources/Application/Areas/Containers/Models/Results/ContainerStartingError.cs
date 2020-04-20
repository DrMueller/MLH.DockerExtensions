using System.Collections.Generic;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Results
{
    public class ContainerStartingError
    {
        public ContainerStartingError(IReadOnlyCollection<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public IReadOnlyCollection<string> ErrorMessages { get; }
    }
}