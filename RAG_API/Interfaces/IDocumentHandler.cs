namespace RAG_API.Interfaces
{
    public interface IDocumentHandler
    {
        Task ProcessDocument(IFormFile formFile);
    }
}