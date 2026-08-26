using Application.Flowdesk.DTO.Projects;
using FlowDeskAPI.DTO.Autentification;

namespace Application.Flowdesk.DTO.Teams
{
    public class TeamResponse
    {
        public string Name { get; set; }
        public IEnumerable<ProjectResponse> Projects { get; set; }
        public IEnumerable<UserResponse> Members { get; set; }
    }
}
