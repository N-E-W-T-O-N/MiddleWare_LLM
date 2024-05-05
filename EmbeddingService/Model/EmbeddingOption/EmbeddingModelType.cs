namespace EmbeddingService.Model.EmbeddingOption
{
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
