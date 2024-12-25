namespace ChatService.Model
{
    public class ChatModel
    {
        public string Type { get; set; } = ModelServiceType.AzureOpenAI;

        [System.Text.Json.Serialization.JsonPropertyName("AzureOpenAI")]
        public AzureOpenAIOptions AzureOpenAI { get; set; }
        public HFOption HuggingFace { get; set; }
        public OpenAIOptions OpenAI { get; set; }
        public OllamaOption Ollama { get; set; }
    }

    public class ModelServiceType
    {
        public const string AzureOpenAI = "AzureOpenAI";
        public const string OpenAI = "OpenAI";
        public const string HF = "HuggingFace";
        public const string Ollama = "Ollama";
    }

    public record AzureOpenAIOptions
    {
        public string APIKey { get; set; }
        public string EndPoint { get; set; }
        public string Deployment { get; set; }
        public int MaxRetries { get; set; }

    }

    public record OpenAIOptions
    {
        public string APIKey { get; set; } = string.Empty;
        public string Deployment { get; set; }
    }

    public record HFOption
    {
        public string ModelID { get; set; }
        public string URL { get; set; } = "https://api-inference.huggingface.co";
        public string ApiKey { get; set; }
    }
    public record OllamaOption
    {
        public string Model { get; set; } = "phi3";
        public string URL { get; set; } = "http://localhost::3000";
    }


}
