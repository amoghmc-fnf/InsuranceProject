using System.Text.Json.Serialization;

namespace AdminApp.Models
{
    public class Admin
    {
        [JsonPropertyName("adminId")]
        public int AdminId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; } = null!;

    }
}
