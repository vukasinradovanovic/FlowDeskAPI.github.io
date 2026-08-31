using Domain.ProjectTracking;

namespace DataAccess.FlowDesk.Seeders.SeedData
{
    public class ProjectSeeder : IDataSeeder<Project>
    {
        public IEnumerable<Project> GetSeedData()
        {
            return new List<Project>
            {
                new Project { Name = "Marketing Website Redesign", Slug = "marketing-website-redesign", Icon = "bi-palette", Theme = "primary", DueDate = DateTime.Now.AddDays(10), CreatedAt = DateTime.Now.AddDays(-30), StatusId = 3 },
                new Project { Name = "Mobile App V3.2", Slug = "mobile-app-v3-2", Icon = "bi-phone", Theme = "emerald", DueDate = DateTime.Now.AddDays(15), CreatedAt = DateTime.Now.AddDays(-25), StatusId = 4 },
                new Project { Name = "AI Chatbot Integration", Slug = "ai-chatbot-integration", Icon = "bi-cpu", Theme = "indigo", DueDate = DateTime.Now.AddDays(20), CreatedAt = DateTime.Now.AddDays(-20), StatusId = 1 },
                new Project { Name = "E-Commerce Checkout Refactor", Slug = "e-commerce-checkout-refactor", Icon = "bi-cart-check", Theme = "rose", DueDate = DateTime.Now.AddDays(5), CreatedAt = DateTime.Now.AddDays(-18), StatusId = 2 },
                new Project { Name = "Cloud Infrastructure Migration", Slug = "cloud-infrastructure-migration", Icon = "bi-cloud-upload", Theme = "amber", DueDate = DateTime.Now.AddDays(45), CreatedAt = DateTime.Now.AddDays(-15), StatusId = 1 },
                new Project { Name = "Design System Component Library", Slug = "design-system-component-library", Icon = "bi-brush", Theme = "emerald", DueDate = DateTime.Now.AddDays(12), CreatedAt = DateTime.Now.AddDays(-14), StatusId = 3 },
                new Project { Name = "OAuth2 & SSO Implementation", Slug = "oauth2-sso-implementation", Icon = "bi-shield-lock", Theme = "indigo", DueDate = DateTime.Now.AddDays(8), CreatedAt = DateTime.Now.AddDays(-12), StatusId = 2 },
                new Project { Name = "Customer Analytics Dashboard", Slug = "customer-analytics-dashboard", Icon = "bi-graph-up-arrow", Theme = "primary", DueDate = DateTime.Now.AddDays(25), CreatedAt = DateTime.Now.AddDays(-10), StatusId = 1 },
                new Project { Name = "GDPR Compliance Audit", Slug = "gdpr-compliance-audit", Icon = "bi-file-earmark-check", Theme = "rose", DueDate = DateTime.Now.AddDays(30), CreatedAt = DateTime.Now.AddDays(-9), StatusId = 3 },
                new Project { Name = "Q4 Social Media Campaign", Slug = "q4-social-media-campaign", Icon = "bi-megaphone", Theme = "amber", DueDate = DateTime.Now.AddDays(18), CreatedAt = DateTime.Now.AddDays(-8), StatusId = 1 },
                new Project { Name = "Automated E2E Testing Suite", Slug = "automated-e2e-testing-suite", Icon = "bi-gear-wide-connected", Theme = "emerald", DueDate = DateTime.Now.AddDays(14), CreatedAt = DateTime.Now.AddDays(-7), StatusId = 2 },
                new Project { Name = "Payment Gateway Webhook Sync", Slug = "payment-gateway-webhook-sync", Icon = "bi-credit-card", Theme = "primary", DueDate = DateTime.Now.AddDays(3), CreatedAt = DateTime.Now.AddDays(-6), StatusId = 4 },
                new Project { Name = "User Onboarding Flow Redesign", Slug = "user-onboarding-flow-redesign", Icon = "bi-person-plus", Theme = "indigo", DueDate = DateTime.Now.AddDays(22), CreatedAt = DateTime.Now.AddDays(-5), StatusId = 1 },
                new Project { Name = "GraphQL Gateway Migration", Slug = "graphql-gateway-migration", Icon = "bi-diagram-3", Theme = "amber", DueDate = DateTime.Now.AddDays(35), CreatedAt = DateTime.Now.AddDays(-4), StatusId = 1 },
                new Project { Name = "Internal Portal Microservices", Slug = "internal-portal-microservices", Icon = "bi-box-seam", Theme = "emerald", DueDate = DateTime.Now.AddDays(40), CreatedAt = DateTime.Now.AddDays(-3), StatusId = 2 },
                new Project { Name = "SEO Performance Optimization", Slug = "seo-performance-optimization", Icon = "bi-search", Theme = "rose", DueDate = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now.AddDays(-2), StatusId = 3 },
                new Project { Name = "Database Indexing & Tuning", Slug = "database-indexing-tuning", Icon = "bi-database-gear", Theme = "primary", DueDate = DateTime.Now.AddDays(16), CreatedAt = DateTime.Now.AddDays(-1), StatusId = 1 },
                new Project { Name = "iOS Push Notifications Service", Slug = "ios-push-notifications-service", Icon = "bi-bell", Theme = "indigo", DueDate = DateTime.Now.AddDays(11), CreatedAt = DateTime.Now, StatusId = 2 },
                new Project { Name = "Brand Identity Refresh 2026", Slug = "brand-identity-refresh-2026", Icon = "bi-vector-pen", Theme = "emerald", DueDate = DateTime.Now.AddDays(60), CreatedAt = DateTime.Now, StatusId = 1 },
                new Project { Name = "Penetration Testing & Hardening", Slug = "penetration-testing-hardening", Icon = "bi-bug", Theme = "rose", DueDate = DateTime.Now.AddDays(19), CreatedAt = DateTime.Now, StatusId = 3 },
                new Project { Name = "API Rate Limiting & Throttling", Slug = "api-rate-limiting-throttling", Icon = "bi-speedometer2", Theme = "amber", DueDate = DateTime.Now.AddDays(13), CreatedAt = DateTime.Now, StatusId = 4 },
                new Project { Name = "Knowledge Base & Docs Portal", Slug = "knowledge-base-docs-portal", Icon = "bi-book", Theme = "primary", DueDate = DateTime.Now.AddDays(28), CreatedAt = DateTime.Now, StatusId = 1 },
                new Project { Name = "Zero Trust Network Access", Slug = "zero-trust-network-access", Icon = "bi-key", Theme = "indigo", DueDate = DateTime.Now.AddDays(50), CreatedAt = DateTime.Now, StatusId = 2 },
                new Project { Name = "Email Marketing Automation Engine", Slug = "email-marketing-automation-engine", Icon = "bi-envelope-paper", Theme = "amber", DueDate = DateTime.Now.AddDays(21), CreatedAt = DateTime.Now, StatusId = 1 },
                new Project { Name = "Kubernetes Cluster Upgrade", Slug = "kubernetes-cluster-upgrade", Icon = "bi-server", Theme = "emerald", DueDate = DateTime.Now.AddDays(9), CreatedAt = DateTime.Now, StatusId = 3 }
            };
        }
    }
}
