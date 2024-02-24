using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models.Configuration
{
    public class ApplicationOptions
    {
        public string DataSource { get; set; }
        public int CachingExpirationTimeInSeconds { get; set; }
        public WeightsConfiguration WeightsConfiguration { get; set; }
    }

    public partial class WeightsConfiguration
    {
        [JsonPropertyName("Building")]
        public Building Building { get; set; }

        [JsonPropertyName("Lock")]
        public Lock Lock { get; set; }

        [JsonPropertyName("Group")]
        public Group Group { get; set; }

        [JsonPropertyName("Medium")]
        public Medium Medium { get; set; }
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
        public Building BuildingId { get; set; }

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
        public Group GroupId { get; set; }

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