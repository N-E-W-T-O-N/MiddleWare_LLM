using ChatManagement.Model;
using ChatManagement.Model.Chat;
using ChatManagement.Service.Chat;
using Newtonsoft.Json;
using RAG_API.Interfaces;

namespace RAG_API.Service
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
