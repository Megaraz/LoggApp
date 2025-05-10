using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Weather
{
    public class TemperatureBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("min")]
        public double? Min { get; set; }

        [JsonPropertyName("max")]
        public double? Max { get; set; }

        [JsonPropertyName("afternoon")]
        public double? Afternoon { get; set; }

        [JsonPropertyName("night")]
        public double? Night { get; set; }

        [JsonPropertyName("evening")]
        public double? Evening { get; set; }

        [JsonPropertyName("morning")]
        public double? Morning { get; set; }
    }

}
