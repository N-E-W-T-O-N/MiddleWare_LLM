namespace ChatManagement.Model.Chat;

public class ChatOutput
{
    public Message OutputMessage { get; set; }
    public int? TotalToken { get; set; }
    public string? error { get; set; }
}