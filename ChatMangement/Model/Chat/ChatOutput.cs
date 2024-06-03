using ChatManagement.Model.Chat;

namespace ChatManagement.Model;

public class ChatOutput
{
    public Message OutputMessage { get; set; }
    public int TotalToken { get; set; }
}