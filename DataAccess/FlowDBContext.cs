using DataAccess.FlowDesk.Configurations.Project_Tracking_Configurations;
using Domain.Identity;
using Domain.ProjectTracking;
using Domain.Statuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.FlowDesk
{
    public class FlowDbContext : DbContext
    {
        private readonly string? _connString;

        public FlowDbContext(DbContextOptions<FlowDbContext> options) : base(options)
        {
        }

        public FlowDbContext(string connString)
        {
            _connString = connString;
        }

        public FlowDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString = _connString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    var configuration = new ConfigurationBuilder()  
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();

                    connectionString = configuration.GetConnectionString("DefaultConnection");
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Flowith;TrustServerCertificate=true;Integrated security=true";
                }

                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlowDbContext).Assembly );
           base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }

    }
}
