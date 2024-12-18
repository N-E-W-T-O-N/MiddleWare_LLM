namespace EmbeddingService.Model.MemoryOption;

public record QdrantStoreConfig
{
    public string EndPoint { get; set; } = "http://localhost:6333/";
    public int Size { get; set; } = 1234;
}