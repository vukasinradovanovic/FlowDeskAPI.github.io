namespace Application.Flowdesk.Settings
{
    public class DefaultPermissionSettings
    {
        public int GuestPermissionId { get; set; }
        public string GuestPermissionName { get; set; }

        // Projects
        public int ViewUserProjectsId { get; set; }
        public string ViewUserProjectsName { get; set; }
        public int ViewProjectsId { get; set; }
        public string ViewProjectsName { get; set; }
        public int CreateProjectsId { get; set; }
        public string CreateProjectsName { get; set; }
        public int EditProjectsId { get; set; }
        public string EditProjectsName { get; set; }
        public int DeleteProjectsId { get; set; }
        public string DeleteProjectsName { get; set; }

        // Teams
        public int CreateTeamsId { get; set; }
        public string CreateTeamsName { get; set; }
        public int EditTeamsId { get; set; }
        public string EditTeamsName { get; set; }
        public int DeleteTeamsId { get; set; }
        public string DeleteTeamsName { get; set; }
        public int ViewTeamsId { get; set; }
        public string ViewTeamsName { get; set; }
    }
}
