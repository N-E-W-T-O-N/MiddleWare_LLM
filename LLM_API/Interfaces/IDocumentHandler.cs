namespace LLM_API.Interfaces
{
    public interface IDocumentHandler
    {
        Task<string> ProcessDocument(IFormFile formFile);
    }
}