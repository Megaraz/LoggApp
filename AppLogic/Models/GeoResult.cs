using System.Text.Json.Serialization;

namespace AppLogic.Models
{
    /// <summary>
    /// Data fetched from open-meteo.com API, representing geographical information about a location.
    /// JSON properties are named and mapped to match the API response structure.
    /// </summary>
    public class GeoResult
    {

        //[JsonPropertyName("id")]
        //public int? Id { get; set; }


        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("latitude")]
        public double? Lat { get; set; }

        [JsonPropertyName("longitude")]
        public double? Lon { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("timezone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("admin1")]
        public string? Admin1 { get; set; }

        [JsonPropertyName("admin2")]
        public string? Admin2 { get; set; }



        [JsonPropertyName("postcodes")]
        public List<string>? PostCodes { get; set; } = new List<string>();

        public override string ToString()
        {
            string postCodes = string.Empty;
            if (PostCodes!.Count > 0)
            {
                postCodes = string.Join("\n", PostCodes);
            }
            return $"{Country}\n{Admin1}\n{Admin2}\nName: {Name}\n{postCodes}";

        }
    }
}
