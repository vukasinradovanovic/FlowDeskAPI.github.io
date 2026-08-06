using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.FlowDesk.Configurations.Identity_Configuration
{
    public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
    {
        public void Configure(EntityTypeBuilder<AuthToken> builder)
        {
            builder.Property(x => x.TokenId).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.TokenId).IsUnique()
                   .IncludeProperties(x => x.InvalidatedAt);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.JwtToken)
                   .WithOne(r => r.RefreshToken)
                   .HasForeignKey<AuthToken>(r => r.BaseTokenId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
