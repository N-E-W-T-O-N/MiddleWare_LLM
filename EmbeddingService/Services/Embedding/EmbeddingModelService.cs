using EmbeddingService.Model.EmbeddingOption;

namespace EmbeddingService.Services.Embedding;

/// <summary>
/// Configuration settings for the chat store.
/// </summary>
public class EmbeddingModelService
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