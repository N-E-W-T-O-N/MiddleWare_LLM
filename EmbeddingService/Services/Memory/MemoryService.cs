using EmbeddingService.Model.MemoryOption;

namespace EmbeddingService.Services.Memory
{
    public class MemoryService
    {
        public static string PropertyName = "Persistent";
        public string StoreType { get; set; } = StorageServiceProviders.Qdrant;

        public QdrantStoreConfig? Qdrant { get; set; }
    }
}
