using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddingService.Model.EmbeddingOption
{
    public class AzureOpenAIOptions
    {
        /// <summary>
        /// Azure OpenAI endpoint URL
        /// </summary>
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// Azure OpenAI deployment name
        /// </summary>
        public string Deployment { get; set; } = string.Empty;

        public string Embedding { get; set; } = "text-embedding-ada-002";

        /// <summary>
        /// API key, required if Auth == APIKey
        /// </summary>
        public string APIKey { get; set; } = string.Empty;

        /// <summary>
        /// How many times to retry in case of throttling.
        /// </summary>
        public int MaxRetries { get; set; } = 10;
    }
}
