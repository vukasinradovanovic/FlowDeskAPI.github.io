using Domain.ProjectTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.FlowDesk.Configurations.Project_Tracking_Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<ProjectAttachment>
    {
        public void Configure(EntityTypeBuilder<ProjectAttachment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.OriginalFileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.FileSize)
                .IsRequired();

            builder.Property(a => a.UploadedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
