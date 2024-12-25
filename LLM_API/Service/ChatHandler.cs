using ChatManagement.Interface;
using ChatManagement.Model.Chat;
using LLM_API.Interfaces;

namespace LLM_API.Service
{
    public class ChatHandler(IChatManagement chatManagement) : IChatHandler
    {
        public async Task<ChatOutput> ProcessChat(ChatInput chatInput)
        {
            try
            {
                ChatOutput result = await chatManagement.ChatHandling(chatInput);

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
        }
    }
}
