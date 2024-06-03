using ChatManagement.Model;
using ChatManagement.Model.Chat;
using Microsoft.AspNetCore.Mvc;
using RAG_API.Interfaces;
using RAG_API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RAG_API.Controllers
{

    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatHandler _chatHandler;

        public ChatController(IChatHandler chatHandler)
        {
            _chatHandler = chatHandler;
        }
        // POST api/<ChatController>
        //[Route("chat")]
        //[ServiceFilter(typeof(ChatInputValidationFilter))]
        [Route("api/chat")]
        [HttpPost]
        public async Task<ChatOutput> HandleChat([FromBody] ChatInput value)
        {
            try
            {
                if (value is null)
                    throw new ArgumentNullException();

                ChatOutput result = await _chatHandler.ProcessChat(value);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
