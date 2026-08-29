using Domain.ProjectTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.FlowDesk.Configurations.Project_Tracking_Configurations
{
    public class ProjectTeamConfiguration : IEntityTypeConfiguration<ProjectTeam>
    {
        public void Configure(EntityTypeBuilder<ProjectTeam> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Id)
                   .HasColumnOrder(0)
                   .ValueGeneratedOnAdd();

            builder.Property(pt => pt.ProjectId).HasColumnOrder(1);
            builder.Property(pt => pt.TeamId).HasColumnOrder(2);

            builder.HasIndex(pt => new { pt.ProjectId, pt.TeamId })
                   .IsUnique();
        }
    }
}
