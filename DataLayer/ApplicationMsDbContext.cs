using EntityLayer.PanteonEntity.MsEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class ApplicationMsDbContext : DbContext
    {
        private static IConfiguration _configuration;

        public ApplicationMsDbContext(DbContextOptions<ApplicationMsDbContext> options) : base(options)
        {
        }

        public ApplicationMsDbContext()
        {
        }

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration?.GetConnectionString("DefaultConnection");
                if (connectionString != null)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }

        public DbSet<PanteonUser> PanteonUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}