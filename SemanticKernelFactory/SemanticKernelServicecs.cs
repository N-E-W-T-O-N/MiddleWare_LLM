using EmbeddingService.Services.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Memory;

namespace SemanticKernelFactory;

public class SkFactory
{
    static SkFactory()
    {
    }
#pragma warning disable SKEXP0001
    public static ISemanticTextMemory CreateSkMemory(ConfigurationManager config)

    {
        return new MemoryBuilder()
            .WithTextEmbeddingGeneration(ServiceExtension.EmbeddingGenerator(config))
            .WithMemoryStore(ServiceExtension.PersistentMemory(config))
            .Build();

    }
}