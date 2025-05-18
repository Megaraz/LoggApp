using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace AppLogic.Models
{
    public class GeoResultResponse
    {
        [JsonPropertyName("results")]
        public List<GeoResult> Results { get; set; } = new();

        [JsonPropertyName("generationtime_ms")]
        public double? GenerationTimeMs { get; set; }
    }
}
