using ChatManagement.Interface;
using ChatManagement.Model;
using ChatManagement.Model.Chat;
using LLM_API.Interfaces;
using Newtonsoft.Json;

namespace LLM_API.Service
{
    public class ChatHandler(IChatManagement chatManagement) : IChatHandler
    {
        private readonly IChatManagement _chatManagement = chatManagement;

        public async Task<ChatOutput> ProcessChat(ChatInput chatInput)
        {
            try
            {

                ChatOutput result = await _chatManagement.ChatHandling(chatInput);

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
    }
}
