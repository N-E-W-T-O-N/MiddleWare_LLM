using ChatManagement.Model.Chat;

namespace LLM_API.Interfaces;

public interface IChatHandler
{
    Task<ChatOutput> ProcessChat(ChatInput value);
}