using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.FlowDesk.Configurations.Identity_Configuration
{
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Id)
                   .HasColumnOrder(0)
                   .ValueGeneratedOnAdd();

            builder.Property(pt => pt.UserId).HasColumnOrder(1);
            builder.Property(pt => pt.TeamId).HasColumnOrder(2);
            builder.HasKey(ut => new { ut.UserId, ut.TeamId });
        }
    }
}
