using System.Text.Json.Serialization;

namespace DataCollectorFunctions.DataTransferObjects
{
    public class Movie
    {
        [JsonPropertyName("adult")]
        public bool IsAdult { get; set; }

        public int Id { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("popularity")]
        public decimal Popularity { get; set; }

        public bool Video { get; set; }
    }
}
