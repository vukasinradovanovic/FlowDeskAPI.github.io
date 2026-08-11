using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.FlowDesk.Configurations.Identity_Configuration
{
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(20).IsRequired();
            builder.Property(x => x.UseCaseName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.UseCaseData).HasColumnType("TEXT");
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.CreatedAt).IncludeProperties("Username", "UseCaseName", "IsSuccessfull");
            builder.HasIndex("CreatedAt", "Username", "UseCaseName").IncludeProperties(x => x.IsSuccessfull);

        }
    }
}
