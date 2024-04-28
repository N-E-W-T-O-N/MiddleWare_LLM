using Azure.AI.OpenAI;
using RAG_API.Interfaces;

namespace RAG_API.Service
{
    public class DocumentHandler : IDocumentHandler
    {
        private readonly List<string> TypeSupported = [".txt", ".pdf", ".docx"];
        private readonly EmbeddingGenerator _embedding;
        private readonly string uploadsFolder;

        public DocumentHandler(EmbeddingGenerator embedding)
        {
            _embedding = embedding;
            uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(uploadsFolder);
        }
        public async Task ProcessDocument(IFormFile formFile)
        {
            if (TypeSupported.Contains(formFile.ContentType))
            {
                var filePath = Path.Combine(uploadsFolder, formFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                var fileInfo = new FileInfo(filePath);

                _embedding.GenerateEmbedding(fileInfo);
            }
            else throw new ArgumentException("Current Extension Not supported");
        }
    }
}
