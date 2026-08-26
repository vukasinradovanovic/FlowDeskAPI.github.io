namespace Application.Flowdesk.DTO.Projects
{
    public class ProjectResponse
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Icon { get; set; }
        public string Theme { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
