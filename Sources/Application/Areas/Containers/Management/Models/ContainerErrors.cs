using System.Collections.Generic;
using JetBrains.Annotations;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Management.Models
{
    [PublicAPI]
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