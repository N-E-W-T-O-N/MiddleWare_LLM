namespace EmbeddingService.Model.EmbeddingOption
{
    public class OllamaConfig
    {
        public string? endpoint { get; set; } = string.Empty;
        public string model { get; set; } = string.Empty;
        public float temprature { get; set; } = 0.0f;
        public int maxtoken { get; set; } = -1;
    }
}
