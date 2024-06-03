using ChatManagement.Model;
using ChatManagement.Model.Chat;

namespace RAG_API.Interfaces;

public interface IChatHandler
{
    Task<ChatOutput> ProcessChat(ChatInput value);
}