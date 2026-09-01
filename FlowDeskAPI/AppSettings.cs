namespace FlowDeskAPI
{
    public class AppSettings
    {
        public string FromEmail { get; set; }
        public string ConnString { get; set; }
        public IEnumerable<string> ApiKeys { get; set; }
        public JwtSettings JwtSettings { get; set; }

        public EmailSettings EmailSettings { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public int ExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }

    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string AppPassword { get; set; }
    }
}
