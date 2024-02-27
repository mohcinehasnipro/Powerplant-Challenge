using System.Text.Json.Serialization;

namespace Challenge.Service.Dtos
{
    public class PowerPlant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }

        [JsonPropertyName("pmin")]
        public int Pmin { get; set; }

        [JsonPropertyName("pmax")]
        public int Pmax { get; set; }


        public double OperatingCost { get; set; }
        public double LoadAssigned { get; set; }
    }


}
