using System.Text.Json;

namespace Application.Flowdesk.DTO.Permissions
{
    public class UseCaseLogResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Action { get; set; }
        public string RawData { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

        public object? Data
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RawData)) return null;
                try
                {
                    return JsonSerializer.Deserialize<object>(RawData);
                }
                catch
                {
                    return RawData;
                }
            }
        }
    }
}
