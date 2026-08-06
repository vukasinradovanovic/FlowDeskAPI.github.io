using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Connections.Identity_Configuration
{
    public class UserRolePermissionConfiguration : IEntityTypeConfiguration<UserRolePermission>
    {
        public void Configure(EntityTypeBuilder<UserRolePermission> builder)
        {
            builder.HasOne(urp => urp.UserRole)
                   .WithMany(ur => ur.UserRolePermissions)
                   .HasForeignKey(urp => urp.UserRoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(urp => urp.Permission)
                   .WithMany(p => p.UserRolePermissions)
                   .HasForeignKey(urp => urp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(urp => new { urp.UserRoleId, urp.PermissionId })
                   .IsUnique();
        }
    }
}
