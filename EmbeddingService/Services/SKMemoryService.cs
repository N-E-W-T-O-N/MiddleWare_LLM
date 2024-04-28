using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Memory;
using RAG_API.Service;

namespace EmbeddingService.Services
{
    public class SKMemoryService:ILoadMemoryService
    {
        private readonly IConfiguration _config;
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0020
        private ISemanticTextMemory textMemory;
        
 

        public SKMemoryService(IConfiguration config)
        {
            _config = config;
            
        }

        public Task<string> GenerateEmbedding(string info)
        {
            //FIRST CHUNK THE STRING
           textMemory = new MemoryBuilder()
            .WithMemoryStore(new QdrantMemoryStore("http://localhost:8000",1423))
            .WithOpenAITextEmbeddingGeneration("", "")
            .Build();
            //

            return Task.Run(()=> String.Empty);
        }
    }
}
