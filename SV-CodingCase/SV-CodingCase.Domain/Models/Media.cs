﻿using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models
{
    public partial class Media
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("groupId")]
        public Guid GroupId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        [JsonPropertyName("owner")]
        public string Owner { get; set; } = default!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set; } = default!;
    }
}
