using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models
{
    public class DataFile
    {
        [JsonPropertyName("buildings")]
        public List<Building> Buildings { get; set; }

        [JsonPropertyName("locks")]
        public List<Lock> Locks { get; set; }

        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }

        [JsonPropertyName("media")]
        public List<Media> Media { get; set; }
    }
}
