using System.IO.Pipelines;
using System.Net;
using LLM_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LLM_API.Controllers
{

    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IDocumentHandler _documentHandler;

        public FilesController(IDocumentHandler documentHandler)
        {
            _documentHandler = documentHandler;
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
        // Version
        /*
        [Route("api/streaming")]
        [HttpPost]
        public async Task PostFileStreamTask(PipeReader boby)
        {
            string tempfile = "";
            using var stream = System.IO.File.OpenWrite(tempfile);
            await boby.CopyToAsync(stream);
        }*/



    }
}
