using Microsoft.AspNetCore.Mvc;
using RAG_API.Interfaces;
using RAG_API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAG_API.Controllers
{

    [ApiController]
    public class FilesContoller : ControllerBase
    {
        [Route("api/file")]
        // POST api/<FilesContoller>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file, IDocumentHandler documentHandler)
        {
            //IF NO FILE IS SEND THROW ERROR
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            try
            {
                await documentHandler.ProcessDocument(file);
                return Ok(file);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
