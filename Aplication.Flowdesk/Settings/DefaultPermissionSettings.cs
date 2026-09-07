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

        // Tasks
        public int ViewUserTasksId { get; set; }
        public string ViewUserTasksName { get; set; }
        public int ViewTasksId { get; set; }
        public string ViewTasksName { get; set; }
        public int CreateTasksId { get; set; }
        public string CreateTasksName { get; set; }
        public int EditTasksId { get; set; }
        public string EditTasksName { get; set; }
        public int DeleteTasksId { get; set; }
        public string DeleteTasksName { get; set; }


        // Teams
        public int CreateTeamsId { get; set; }
        public string CreateTeamsName { get; set; }
        public int EditTeamsId { get; set; }
        public string EditTeamsName { get; set; }
        public int DeleteTeamsId { get; set; }
        public string DeleteTeamsName { get; set; }
        public int ViewTeamsId { get; set; }
        public string ViewTeamsName { get; set; }

        // Assigments
        public int CanAssignTasksId { get; set; }
        public string CanAssignTasksName { get; set; }
        public int CanAssignTeamsId { get; set; }
        public string CanAssignTeamsName { get; set; }
        public int CanViewUseCaseLogsId { get; set; }
        public string CanViewUseCaseLogsName { get; set; }
    }
}
