using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Managemnet.Models
{
    public class CreatedContainer
    {
        public string Id { get; }

        public CreatedContainer(string id)
        {
            Guard.StringNotNullOrEmpty(() => id);

            Id = id;
        }
    }
}