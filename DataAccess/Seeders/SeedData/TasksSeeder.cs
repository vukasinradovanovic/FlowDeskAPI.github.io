using Domain.ProjectTracking;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class TasksSeeder : IDataSeeder<ProjectTask>
    {
        public IEnumerable<ProjectTask> GetSeedData()
        {
            return new List<ProjectTask>()
{
    // Project 1 Tasks (Core App Setup)
    new ProjectTask
    {
        Name = "Setup JWT Authentication",
        Slug = "setup-jwt-authentication",
        Description = "Implement JWT token generation, refresh tokens, and middleware validation for secure API access.",
        CreatedAt = DateTime.UtcNow.AddDays(-10),
        UpdatedAt = DateTime.UtcNow.AddDays(-8),
        StatusId = 5, // Done
        AssignedUserId = 1,
        ProjectId = 1
    },
    new ProjectTask
    {
        Name = "Design Database Schema",
        Slug = "design-database-schema",
        Description = "Create initial EF Core migration for Users, Teams, Projects, and Permissions tables.",
        CreatedAt = DateTime.UtcNow.AddDays(-9),
        UpdatedAt = DateTime.UtcNow.AddDays(-7),
        StatusId = 5, // Done
        AssignedUserId = 2,
        ProjectId = 1
    },
    new ProjectTask
    {
        Name = "Implement Role-Based Authorization",
        Slug = "implement-role-based-authorization",
        Description = "Build custom permission handlers to validate user actions across project endpoints.",
        CreatedAt = DateTime.UtcNow.AddDays(-5),
        UpdatedAt = DateTime.UtcNow.AddDays(-2),
        StatusId = 3, // In Progress
        AssignedUserId = 1,
        ProjectId = 1
    },

    // Project 2 Tasks (Notification System)
    new ProjectTask
    {
        Name = "Integrate SmtpEmailSender",
        Slug = "integrate-smtp-email-sender",
        Description = "Configure MailKit/SmtpClient with HTML template parsing for account registration emails.",
        CreatedAt = DateTime.UtcNow.AddDays(-12),
        UpdatedAt = DateTime.UtcNow.AddDays(-11),
        StatusId = 5, // Done
        AssignedUserId = 3,
        ProjectId = 2
    },
    new ProjectTask
    {
        Name = "Build Email Template Composer",
        Slug = "build-email-template-composer",
        Description = "Set up Handlebars.NET engine to read and populate HTML templates for activation and registration.",
        CreatedAt = DateTime.UtcNow.AddDays(-6),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 4, // In Review
        AssignedUserId = 4,
        ProjectId = 2
    },

    // Project 3 Tasks (Dashboard UI)
    new ProjectTask
    {
        Name = "Create Kanban Task Board",
        Slug = "create-kanban-task-board",
        Description = "Build interactive drag-and-drop board displaying tasks filtered by 5 status columns.",
        CreatedAt = DateTime.UtcNow.AddDays(-4),
        UpdatedAt = DateTime.UtcNow.AddDays(-4),
        StatusId = 2, // To Do
        AssignedUserId = 2,
        ProjectId = 3
    },
    new ProjectTask
    {
        Name = "Implement Analytics Widgets",
        Slug = "implement-analytics-widgets",
        Description = "Display active user counts, completed project tasks, and velocity charts.",
        CreatedAt = DateTime.UtcNow.AddDays(-3),
        UpdatedAt = DateTime.UtcNow.AddDays(-3),
        StatusId = 1, // Backlog
        AssignedUserId = 3,
        ProjectId = 3
    },

    // Project 4 Tasks (Team Management)
    new ProjectTask
    {
        Name = "User Activation State Workflow",
        Slug = "user-activation-state-workflow",
        Description = "Handle email token click transitions, validate 15-min expiration, and redirect users to login.",
        CreatedAt = DateTime.UtcNow.AddDays(-2),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 3, // In Progress
        AssignedUserId = 1,
        ProjectId = 4
    },
    new ProjectTask
    {
        Name = "Add Team Invitation System",
        Slug = "add-team-invitation-system",
        Description = "Allow team leads to generate invite links with custom roles assigned to new members.",
        CreatedAt = DateTime.UtcNow.AddDays(-1),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 1, // Backlog
        AssignedUserId = 4,
        ProjectId = 4
    },

    // Project 5 Tasks (API Infrastructure)
    new ProjectTask
    {
        Name = "Fix Pagination Logic in Controllers",
        Slug = "fix-pagination-logic-in-controllers",
        Description = "Standardize PageSize, PageNumber, and TotalPages response metadata across all GET list endpoints.",
        CreatedAt = DateTime.UtcNow.AddDays(-15),
        UpdatedAt = DateTime.UtcNow.AddDays(-10),
        StatusId = 5, // Done
        AssignedUserId = 2,
        ProjectId = 5
    },
    new ProjectTask
    {
        Name = "Configure Global Exception Handling",
        Slug = "configure-global-exception-handling",
        Description = "Catch EntityNotFoundException and custom domain exceptions returning structured ProblemDetails JSON.",
        CreatedAt = DateTime.UtcNow.AddDays(-8),
        UpdatedAt = DateTime.UtcNow.AddDays(-4),
        StatusId = 4, // In Review
        AssignedUserId = 1,
        ProjectId = 5
    },

    // Project 6 Tasks (Third-Party Integrations)
    new ProjectTask
    {
        Name = "Configure Webhook Listeners",
        Slug = "configure-webhook-listeners",
        Description = "Receive inbound updates for external task sync and verify payload signatures.",
        CreatedAt = DateTime.UtcNow.AddDays(-5),
        UpdatedAt = DateTime.UtcNow.AddDays(-3),
        StatusId = 2, // To Do
        AssignedUserId = 3,
        ProjectId = 6
    },

    // Project 7 Tasks (Security & Compliance)
    new ProjectTask
    {
        Name = "Audit Password Hashing with BCrypt",
        Slug = "audit-password-hashing-with-bcrypt",
        Description = "Ensure work factors match security standards and update legacy user credentials safely.",
        CreatedAt = DateTime.UtcNow.AddDays(-14),
        UpdatedAt = DateTime.UtcNow.AddDays(-12),
        StatusId = 5, // Done
        AssignedUserId = 4,
        ProjectId = 7
    },

    // Project 8 Tasks (Performance Tuning)
    new ProjectTask
    {
        Name = "Optimize EF Core Query Projections",
        Slug = "optimize-ef-core-query-projections",
        Description = "Replace heavy entity includes with lightweight DTO Select projections to fix slow queries.",
        CreatedAt = DateTime.UtcNow.AddDays(-3),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 3, // In Progress
        AssignedUserId = 2,
        ProjectId = 8
    },

    // Projects 9–15 Coverage
    new ProjectTask
    {
        Name = "Set Up Redis Caching for Teams",
        Slug = "set-up-redis-caching-for-teams",
        Description = "Cache frequently read team project permissions to reduce database hits.",
        CreatedAt = DateTime.UtcNow.AddDays(-7),
        UpdatedAt = DateTime.UtcNow.AddDays(-5),
        StatusId = 2, // To Do
        AssignedUserId = 1,
        ProjectId = 9
    },
    new ProjectTask
    {
        Name = "Refactor DI Setup Extensions",
        Slug = "refactor-di-setup-extensions",
        Description = "Organize command, query, and validator registrations inside SetupApplication clean layer methods.",
        CreatedAt = DateTime.UtcNow.AddDays(-11),
        UpdatedAt = DateTime.UtcNow.AddDays(-9),
        StatusId = 5, // Done
        AssignedUserId = 3,
        ProjectId = 10
    },
    new ProjectTask
    {
        Name = "Implement User Avatar Color Picker",
        Slug = "implement-user-avatar-color-picker",
        Description = "Allow users to pick default hex colors during registration for fallback profile badges.",
        CreatedAt = DateTime.UtcNow.AddDays(-6),
        UpdatedAt = DateTime.UtcNow.AddDays(-2),
        StatusId = 4, // In Review
        AssignedUserId = 4,
        ProjectId = 11
    },
    new ProjectTask
    {
        Name = "Build Export to CSV Feature",
        Slug = "build-export-to-csv-feature",
        Description = "Stream task audit logs directly into CSV format from EF Core async queries.",
        CreatedAt = DateTime.UtcNow.AddDays(-2),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 1, // Backlog
        AssignedUserId = 2,
        ProjectId = 12
    },
    new ProjectTask
    {
        Name = "Setup Docker Compose Environment",
        Slug = "setup-docker-compose-environment",
        Description = "Configure MSSQL container alongside API image for local developer setup.",
        CreatedAt = DateTime.UtcNow.AddDays(-13),
        UpdatedAt = DateTime.UtcNow.AddDays(-12),
        StatusId = 5, // Done
        AssignedUserId = 1,
        ProjectId = 13
    },
    new ProjectTask
    {
        Name = "Integrate Swagger OpenApi Spec",
        Slug = "integrate-swagger-openapi-spec",
        Description = "Annotate API controllers with XML documentation and JWT Bearer security schemes.",
        CreatedAt = DateTime.UtcNow.AddDays(-4),
        UpdatedAt = DateTime.UtcNow.AddDays(-2),
        StatusId = 3, // In Progress
        AssignedUserId = 3,
        ProjectId = 14
    },
    new ProjectTask
    {
        Name = "Load Testing Endpoint Throughput",
        Slug = "load-testing-endpoint-throughput",
        Description = "Benchmark activation link requests and registration command processing under high concurrency.",
        CreatedAt = DateTime.UtcNow.AddDays(-1),
        UpdatedAt = DateTime.UtcNow.AddDays(-1),
        StatusId = 1, // Backlog
        AssignedUserId = 4,
        ProjectId = 15
    }
};
        }
    }
}