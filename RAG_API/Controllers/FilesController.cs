using Microsoft.AspNetCore.Mvc;
using RAG_API.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAG_API.Controllers
{

    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IDocumentHandler _documentHandler;

        public FilesController(IDocumentHandler documentHandler)
        {
            _documentHandler= documentHandler;
        }
        [Route("api/file")]
        // POST api/<FilesContoller>
        // Buffer
        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            //IF NO FILE IS SEND THROW ERROR
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            try
            {
                var result = await _documentHandler.ProcessDocument(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Streaming 
       /*
        [Routing("/streaming")]
        [HttpPost]
        public async Task<IActionResult> PostFileStreamTask()
        {
            var memo = new MemoryStream(); 
            await Request.Body.CopyToAsync(memo);
            throw new NotImplementedException();
        }*/



    }
}
