using SV_CodingCase.Domain.Models.Enums;
using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models
{
    public class Lock
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("buildingId")]
        public Guid BuildingId { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public LockType Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("floor")]
        public string? Floor { get; set; }

        [JsonPropertyName("roomNumber")]
        public string RoomNumber { get; set; }
    }
}
