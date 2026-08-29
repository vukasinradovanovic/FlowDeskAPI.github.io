namespace Application.Flowdesk.DTO.Projects
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Theme { get; set; }
        public DateTime DueDate { get; set; }
        public int TeamId { get; set; }
    }
}
