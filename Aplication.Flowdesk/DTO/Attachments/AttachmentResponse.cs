namespace Application.Flowdesk.DTO.Attachments
{
    public class AttachmentResponse
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
