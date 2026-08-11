namespace Domain.Identity
{
    public class UseCaseLog : BaseEntity
    {
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UseCaseName { get; set; }
        public string UseCaseData { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
