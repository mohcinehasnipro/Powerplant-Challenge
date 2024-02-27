using System.Text.Json.Serialization;

namespace Challenge.Service.Dtos
{
    public class Payload
    {
        [JsonPropertyName("load")]
        public double Load { get; set; }

        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; }

        [JsonPropertyName("powerplants")]
        public List<PowerPlant> Powerplants { get; set; }
    }


}
