using BlingFire;
using EmbeddingService.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.SemanticKernel.Memory;

using Xceed.Words.NET;

namespace EmbeddingService.Services.Embedding
{
#pragma warning disable SKEXP0001
    public class EmbeddingGenerator : IEmbeddingGenerator
    {
        private readonly string collection = "RAGFILECOLLECTION";
        private ISemanticTextMemory _STMemory;

        public EmbeddingGenerator(ISemanticTextMemory STMemory)
        {
            _STMemory = STMemory;
        }
#pragma warning restore SKEXP0001

        public async Task<string> GenerateEmbedding(FileInfo fileInfo)
        {
            try
            {
                string info = ReadFile(fileInfo);

                //string result = await loadMemoryService.GenerateEmbedding(info);

                string[] parseDate = BlingFireUtils2.GetSentences(info).ToArray();

                foreach (var value in parseDate)
                {
                    await _STMemory.SaveInformationAsync(collection, value,
                        Guid.NewGuid().ToString(), additionalMetadata: fileInfo.Name);
                }

                return $"OK:{parseDate.Length}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string ReadFile(FileInfo fileInfo)
        {
            string info = string.Empty;
            //Read a PDF FIle
            //REMARK : ONLY TEXT IS BEEN READ IF YOU WANT TXT+TABLE INFO USE AZURE DOCUMENT INTELLIGENCE
            if (fileInfo.Extension.Equals(".pdf"))
            {
                using (PdfReader reader = new PdfReader(fileInfo.FullName))
                {
                    StringWriter textWriter = new();
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        string text = PdfTextExtractor.GetTextFromPage(reader, page);
                        textWriter.WriteLine(text);
                    }
                    info = textWriter.ToString();
                }
            }
            //For Word Files
            else if (fileInfo.Extension.Equals(".docx"))
            {
                using (DocX doc = DocX.Load(fileInfo.FullName))
                {
                    info = doc.Text;
                }
            }

            // FOR TXT FILE 
            else if (fileInfo.Extension.Equals(".txt") || fileInfo.Extension.Equals(".json"))
            {
                using (TextReader tx = new StreamReader(fileInfo.FullName))
                {
                    info = tx.ReadToEnd();
                }
                //File.ReadAllText(fileInfo.FullName);
            }

            else if (fileInfo.Extension.Equals(".xlsx"))
            { }
            return info;
        }


    }
}
