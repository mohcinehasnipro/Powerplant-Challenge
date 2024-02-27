using System.Text.Json.Serialization;

namespace Challenge.Service.Dtos
{
    public class Response
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("p")]
        public double P { get; set; }
    }
}
