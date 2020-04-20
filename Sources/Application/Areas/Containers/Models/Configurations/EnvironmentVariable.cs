using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Models.Configurations
{
    public class EnvironmentVariable
    {
        public EnvironmentVariable(string key, string value)
        {
            Guard.StringNotNullOrEmpty(() => key);
            Guard.StringNotNullOrEmpty(() => value);

            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}