using EmbeddingService.Model.EmbeddingOption;
using EmbeddingService.Model.MemoryOption;
using EmbeddingService.Services.Embedding;
using EmbeddingService.Services.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;

namespace EmbeddingService.Services.Extension
{
    public static class ServiceExtension
    {
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0070
        public static IMemoryStore PersistentMemory(

            ConfigurationManager configuration)

        {
            MemoryService memStore = new();
            var section = configuration.GetSection(MemoryService.PropertyName);
            section.Bind(memStore);

            IMemoryStore memoryStore = memStore.StoreType switch
            {
                StorageServiceProviders.Qdrant => new QdrantMemoryStore(memStore.Qdrant.EndPoint, memStore.Qdrant.Size),

                _ => throw new NotSupportedException($"{memStore.StoreType} is not supported")
            };
            return memoryStore;
        }
        /// <summary>
        /// Provide Embedding Service
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ITextEmbeddingGenerationService EmbeddingGenerator(ConfigurationManager configuration)
        {

            EmbeddingModelService embedSrv = new();
            var section = configuration.GetSection(EmbeddingModelService.PropertyName);
            section.Bind(embedSrv);


            ITextEmbeddingGenerationService ser = embedSrv.EmbeddingType switch
            {

                EmbeddingServiceType.AzureOpenAI => new AzureOpenAITextEmbeddingGenerationService(embedSrv.AzureOpenAI.Deployment,
                    embedSrv.AzureOpenAI.Endpoint,
                    embedSrv.AzureOpenAI.APIKey),

                EmbeddingServiceType.OpenAI => new OpenAITextEmbeddingGenerationService(embedSrv.OpenAI.EmbeddingModel, embedSrv.OpenAI.APIKey),

                EmbeddingServiceType.HuggingFace => new HuggingFaceTextEmbeddingGenerationService(embedSrv.HuggingFace.ModelID,
                    new Uri(embedSrv.HuggingFace.EndPoint),
                    embedSrv.HuggingFace.ApiKey),

                //EmbeddingType.Ollama => new TextEmbedd,
                _ => throw new ArgumentException("NOT ALL VALES ARE PASSED")

            };

            //      var section = configuration.GetSection(EmbeddingModelType.PropertyName);

            return ser;
        }

#pragma warning restore SKEXP0070
#pragma warning restore SKEXP0020
#pragma warning restore SKEXP0010
#pragma warning restore SKEXP0001

    }
}
