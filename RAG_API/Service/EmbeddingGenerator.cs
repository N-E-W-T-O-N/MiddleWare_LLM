
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Xceed.Words.NET;

namespace RAG_API.Service
{
    public class EmbeddingGenerator
    {
        private readonly string collection = "RAGFILECOLLECTION";

        public EmbeddingGenerator(ILoadMemoryService loadMemoryService) { }
        public async void GenerateEmbedding(FileInfo fileInfo)
        {
            string info = ReadFile(fileInfo);

            //string result = await loadMemoryService.GenerateEmbedding(info);

        }

        private string ReadFile(FileInfo fileInfo)
        {
            string info =string.Empty;
            //Read a PDF FIle
            //REMARK : ONLY TEXT IS BEEN READ IF YOU WANT TXT+TABLE INFO USE AZURE DOCUMENT INTELLIGENCE
            if (fileInfo.Extension.Equals("pdf"))
            {
                using (PdfReader reader = new PdfReader(fileInfo.FullName))
                {
                    StringWriter textWriter = new ();
                    for(int page= 0;page <= reader.NumberOfPages;page++)
                    {
                        string text = PdfTextExtractor.GetTextFromPage(reader, page);
                        textWriter.WriteLine(text);
                    }
                    info = textWriter.ToString();
                }
            }
            //For Word Files
            else if (fileInfo.Extension.Equals("docx"))
            {
                using (DocX doc = DocX.Load(fileInfo.FullName))
                {
                    info = doc.Text;
                }
            }
        
            // FOR TXT FILE 
            else if (fileInfo.Extension.Equals("txt"))
            {
                using (TextReader tx = new StreamReader(fileInfo.FullName))
                {
                    info = tx.ReadToEnd();
                }
                //File.ReadAllText(fileInfo.FullName);
            }
            return info;
        } 
    }
}
