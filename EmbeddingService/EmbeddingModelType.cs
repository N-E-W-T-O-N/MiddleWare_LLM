using EmbeddingService.Model;

namespace EmbeddingService
{
    /// <summary>
    /// Configuration settings for the chat store.
    /// </summary>
    public class EmebeddingModelType
    {
        public const string PropertyName = "Embedding";

        /// <summary>
        /// Gets or sets the type of chat store to use.
        /// </summary>
        public string EmbeddingType { get; set; } = EmbeddingServiceType.AzureOpenAI;

        /// <summary>
        /// Gets or sets the configuration for the AzureOpenAI based embedding.
        /// </summary>
        public AzureOpenAIOptions? AzureOpenAI { get; set; }
        /// <summary>
        /// Gets or sets the configuration for the OpenAI based embedding.
        /// </summary>
        public OpenAIOptions? OpenAI { get; set; }
        /// <summary>
        /// Gets or sets the configuration for the HuggingFaceOptions based embedding.
        /// </summary>
        public HuggingFaceOptions? HuggingFace { get; set; }
        /// <summary>
        /// Gets or sets the configuration for the Ollama based embedding.
        /// </summary>
        public OllamaConfig? Ollama { get; set; }

        
    }

    public class HuggingFaceOptions
    {
        public string ModelID { get; set; }
        public string Uri { get; set; } 
        public string ApiKey { get; set; }
    }

    public class EmbeddingServiceType
    {
        /// <summary>
        /// AZURE OPENAI based embedding
        /// </summary>
       public const string AzureOpenAI = "AzureOpenAI";

        /// <summary>
        /// OpenAI based Embedding
        /// </summary>
        public const string OpenAI = "OpenAI";
        /// <summary>
        /// HuggingFace based Embedding
        /// </summary>
        public const string HuggingFace = "HuggingFace";
        /// <summary>
        /// Ollama based Embedding
        /// </summary>
        public const string Ollama = "Ollama";

        }
}
