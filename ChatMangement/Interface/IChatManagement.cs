using ChatManagement.Model.Chat;

namespace ChatManagement.Interface;

public interface IChatManagement
{
    Task<ChatOutput> ChatHandling(ChatInput value);
}