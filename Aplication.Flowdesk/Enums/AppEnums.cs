using System;
using System.Collections.Generic;
using System.Text;

namespace Aplication.Flowdesk.Enums
{
    // Enumeration for application permissions
    public class AppEnums
    {
        public enum AppPermission
        {
            FullAccess = 1,
            EditProjects = 2,
            DeleteProjects = 3,
            CreateProjects = 4,
            EditTeams = 5,
            DeleteTeams = 6,
            CreateTeams = 7
        }

        public enum AppRole
        {
            Admin = 1,
            ProjectManager = 2,
            TeamLead = 3,
            Developer = 4,
            Tester = 5
        }

        public enum AppStatus
        {
            Active = 1,
            Inactive = 2,
            Pending = 3,
            Suspended = 4
        }

        public enum AppTheme
        {
            Primary = 1,
            Indigo = 2,
            Amber = 3,
            Emerald = 4,
        }
    }
}
