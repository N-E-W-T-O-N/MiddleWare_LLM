namespace EmbeddingService.Interfaces;
public interface IEmbeddingGenerator
{
    public  Task<string> GenerateEmbedding(FileInfo fileInfo);
}