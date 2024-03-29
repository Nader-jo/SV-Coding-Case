﻿using System.Text.Json.Serialization;

namespace SV_CodingCase.Domain.Models
{
    public class Building
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("shortCut")]
        public string ShortCut { get; set; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
    }
}
