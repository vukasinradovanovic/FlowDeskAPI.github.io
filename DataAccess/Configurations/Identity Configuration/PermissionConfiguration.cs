using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Connections.Identity_Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasMany(x => x.UserRolePermissions)
                .WithOne(p => p.Permission)
                .HasForeignKey(p => p.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
