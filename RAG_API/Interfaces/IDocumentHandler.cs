namespace RAG_API.Interfaces
{
    public interface IDocumentHandler
    {
        Task<string> ProcessDocument(IFormFile formFile);
    }
}