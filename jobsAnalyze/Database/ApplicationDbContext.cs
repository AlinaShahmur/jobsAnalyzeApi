using jobsAnalyze.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jobsAnalyze.Database
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _config;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ApplicationStage> ApplicationStages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                        .UseSqlServer(_config.GetConnectionString("JobsAnalyzeDb"),
                            optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
