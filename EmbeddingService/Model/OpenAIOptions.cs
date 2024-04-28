using System.ComponentModel.DataAnnotations;

namespace EmbeddingService.Model
{
    public class OpenAIOptions
    {
        /// <summary>
        /// Model used for text generation. Chat models can be used too.
        /// </summary>
        public string TextModel { get; set; } = string.Empty;


        /// <summary>
        /// Model used to embedding generation/
        /// </summary>
        public string EmbeddingModel { get; set; } = "text-embedding-ada-002";

        /// <summary>
        /// OpenAI API key.
        /// </summary>
        [Required]
        public string APIKey { get; set; } = string.Empty;
    }
}
