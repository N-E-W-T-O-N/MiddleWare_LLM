namespace RAG_API.Service
{
    public interface ILoadMemoryService
    {
        public Task<string> GenerateEmbedding(string info);
    }
}