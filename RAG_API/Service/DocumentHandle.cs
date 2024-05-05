using EmbeddingService.Interfaces;
using RAG_API.Interfaces;

namespace RAG_API.Service
{
    public class DocumentHandler : IDocumentHandler
    {
        private readonly List<string> TypeSupported =
        [
            "text/plain", "application/pdf", "application/json",                                            // Text , Pdf , Json
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",                      // Words
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"                             // SpreadSheet
        ];
        private readonly IEmbeddingGenerator _embedding;
        private readonly string _uploadsFolder = string.Empty;

        public DocumentHandler(IEmbeddingGenerator embedding)
        {
            _embedding = embedding;
            _uploadsFolder = Path.Combine(".", "uploads");//Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(_uploadsFolder);
        }
        public async Task<string> ProcessDocument(IFormFile formFile)
        {

            if (TypeSupported.Contains(formFile.ContentType))
            {
                try
                {
                    var filePath = Path.Combine(_uploadsFolder, formFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var fileInfo = new FileInfo(filePath);

                    string result = await _embedding.GenerateEmbedding(fileInfo);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            throw new ArgumentException($"Following Extension {formFile.ContentType} current Not supported");
        }
    }
}