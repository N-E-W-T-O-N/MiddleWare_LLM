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

    public record AzureOpenAIOptions:Common
    {
        public string APIKey { get; set; }
        public string EndPoint { get; set; }
        public string Deployment { get; set; }

    }

    public record OpenAIOptions:Common
    {
        public string APIKey { get; set; } = string.Empty;
        public string Deployment { get; set; }
    }

    public record HFOption:Common
    {
        public string ModelID { get; set; }
        public string URL { get; set; } = "https://api-inference.huggingface.co";
        public string ApiKey { get; set; }
        public bool UseCache { get; set; }
        public bool Wait { get; set; }
    }
    public record OllamaOption:Common
    {
        public string Model { get; set; } = "phi3";
        public string URL { get; set; } = "http://localhost::3000";
    }

    /// <summary>
    /// Common Setting 
    /// </summary>
    public record Common
    {
        public double? Temperature { get; set; }
        public int? MaxTokens { get; set; }
        public int? MaxRetries { get; set; }
        public double? TopP { get; set; }
        public double? PresencePenality { get; set; }
        public double? FrequencyPenalty { get; set; }
        public IList<string>? Stop { get; set; }
    }


}
