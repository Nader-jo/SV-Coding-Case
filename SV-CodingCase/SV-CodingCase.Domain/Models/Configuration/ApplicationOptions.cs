using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models.Configuration
{
    public class ApplicationOptions
    {
        public string DataSource { get; set; } = default!;
        public int CachingExpirationTimeInSeconds { get; set; }
        public WeightsConfiguration WeightsConfiguration { get; set; } = default!;
    }

    public partial class WeightsConfiguration
    {
        [JsonPropertyName("Building")]
        public Building Building { get; set; } = default!;

        [JsonPropertyName("Lock")]
        public Lock Lock { get; set; } = default!;

        [JsonPropertyName("Group")]
        public Group Group { get; set; } = default!;

        [JsonPropertyName("Medium")]
        public Medium Medium { get; set; } = default!;
    }

    public partial class Building
    {
        [JsonPropertyName("ShortCut")]
        public int ShortCut { get; set; }

        [JsonPropertyName("Name")]
        public int Name { get; set; }

        [JsonPropertyName("Description")]
        public int Description { get; set; }
    }

    public partial class Group
    {
        [JsonPropertyName("Name")]
        public int Name { get; set; }

        [JsonPropertyName("Description")]
        public int Description { get; set; }
    }

    public partial class Lock
    {
        [JsonPropertyName("buildingId")]
        public Building BuildingId { get; set; } = default!;

        [JsonPropertyName("Type")]
        public int Type { get; set; }

        [JsonPropertyName("Name")]
        public int Name { get; set; }

        [JsonPropertyName("SerialNumber")]
        public int SerialNumber { get; set; }

        [JsonPropertyName("Floor")]
        public int Floor { get; set; }

        [JsonPropertyName("RoomNumber")]
        public int RoomNumber { get; set; }

        [JsonPropertyName("Description")]
        public int Description { get; set; }
    }

    public partial class Medium
    {
        [JsonPropertyName("GroupId")]
        public Group GroupId { get; set; } = default!;

        [JsonPropertyName("Type")]
        public int Type { get; set; }

        [JsonPropertyName("Owner")]
        public int Owner { get; set; }

        [JsonPropertyName("SerialNumber")]
        public int SerialNumber { get; set; }

        [JsonPropertyName("Description")]
        public int Description { get; set; }
    }
}