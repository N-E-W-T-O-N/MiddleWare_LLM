using System.Text.Json.Serialization;

namespace ChatManagement.Model.ChatModel
{
    public record AzureOpenAIOptions
    {
        public string APIKey { get; set; }
        public string EndPoint { get; set; }
        public string Deployment { get; set; }
        public int MaxRetries { get; set; }

    }

    public class ChatModel
    {
        public string Type { get; set; } = ModelServiceType.AzureOpenAI;
        [JsonPropertyName("AzureOpenAI")]
        public AzureOpenAIOptions AzureOpenAI { get; set; }
        public OpenAIOptions OpenAI { get; set; }
    }

    public class ModelServiceType
    {
        public const string AzureOpenAI = "AzureOpenAI";
        public const string OpenAI = "OpenAI";
    }
    public record OpenAIOptions
    {

        public string APIKey { get; set; } = string.Empty;
        public string Deployment { get; set; }
    }
}
