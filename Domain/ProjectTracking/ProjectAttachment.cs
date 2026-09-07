namespace Domain.ProjectTracking
{
    public class ProjectAttachment : BaseEntity
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int TaskId { get; set; }
        public virtual ProjectTask Task { get; set; } = null!;
    }
}
