using System;
using JetBrains.Annotations;

namespace Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels
{
    [PublicAPI]
    public class IndividualDataModel
    {
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public long Id { get; set; }
        public string LastName { get; set; }
    }
}