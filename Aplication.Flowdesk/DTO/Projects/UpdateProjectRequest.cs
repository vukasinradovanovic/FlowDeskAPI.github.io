namespace Application.Flowdesk.DTO.Projects
{
    public class UpdateProjectRequest
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Theme { get; set; }
        public DateTime DueDate { get; set; }
        public int TeamId { get; set; }
        public int StatusId { get; set; }
    }
}
