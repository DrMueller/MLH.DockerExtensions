﻿using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlh.DockerExtensions.Areas.Containers.Configurations
{
    public class ImageIdentifier
    {
        public ImageIdentifier(string name, string tag)
        {
            Guard.StringNotNullOrEmpty(() => name);
            Guard.StringNotNullOrEmpty(() => tag);

            Name = name;
            Tag = tag;
        }

        public string Name { get; }
        public string Tag { get; }

        internal string CompleteIdentifier => $"{Name}:{Tag}";
    }
}