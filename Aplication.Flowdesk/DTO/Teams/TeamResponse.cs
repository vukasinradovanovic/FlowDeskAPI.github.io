using Application.Flowdesk.DTO.Projects;
using FlowDeskAPI.DTO.Autentification;

namespace Application.Flowdesk.DTO.TeamDto
{
    public class TeamResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectResponse> Projects { get; set; }
        public IEnumerable<UserResponse> Members { get; set; }
    }
}
