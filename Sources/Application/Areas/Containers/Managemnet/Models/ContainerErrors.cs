using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models
{
    public class ContainerErrors
    {
        public ContainerErrors(params string[] errorMessages)
        {
            Guard.ObjectNotNull(() => errorMessages);

            ErrorMessages = errorMessages;
        }

        public IReadOnlyCollection<string> ErrorMessages { get; }
    }
}