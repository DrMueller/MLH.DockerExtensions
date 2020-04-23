using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DataModels;

namespace Mmu.Mlh.DockerExtensions.FakeApp.Areas.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public DbSet<IndividualDataModel> Individuals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Initial Catalog=TestForDocker;Data Source=LT-R90S3YAQ\SQLEXPRESS");
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualDataModel>().HasKey(f => f.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}