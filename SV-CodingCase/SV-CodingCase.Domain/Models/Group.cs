using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models
{
    public class Group
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

    }
}
