using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.FlowDesk.Connections
{
    public class UserConnection : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasIndex(x => x.Username)
                   .IsUnique();

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(128);

            builder.Property(x => x.AvatarColor)
                   .IsRequired(false)
                   .HasMaxLength(20);

            builder.Property(x => x.ActivationCode)
                .HasMaxLength(60);
            builder.HasIndex(x => x.ActivationCode);

            builder.Property(x => x.RegisteredAt)
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(x => x.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AssignedTasks)
                .WithOne(t => t.AssignedUser)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UserTeams)
                .WithOne(ut => ut.User)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
