using ChatManagement.Model.Chat;
using LLM_API.Interfaces;
using LLM_API.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LLM_API.Controllers
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

        [HttpPost("api/{provider}/chat")]
        [Route("")]
        public async Task<ChatOutput> HandleChat(string provider,[FromBody] ChatInput value)
        {
            string[] possible = ["huggingface", "openai", "azureopenai", "ollama"];
            try
            {
                if (possible.Contains(provider, StringComparer.InvariantCultureIgnoreCase))
                {
                    ChatOutput result = await _chatHandler.ProcessChat(value);
                }
                else
                {
                    return new ChatOutput() { error = "Following Provider not Exist" };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
