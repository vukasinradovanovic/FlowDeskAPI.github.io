namespace Application.Flowdesk.Interfaces
{
    /// <summary>
    /// Contract for the email sending service.
    /// Defines operations for dispatching transactional emails (such as account activation, 
    /// password resets, and notifications) across the application.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends a single email to the specified recipient.
        /// </summary>
        /// <param name="recipient">The target user's email address (e.g., "user@example.com").</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The message payload (can contain plain text or HTML content).</param>
        void SendEmail(string recipient, string subject, string body);
    }
}