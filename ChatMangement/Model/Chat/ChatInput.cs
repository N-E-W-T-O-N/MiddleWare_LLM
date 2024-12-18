namespace ChatManagement.Model.Chat;


public class ChatInput
{
    public List<Message> messages { get; set; }
    public float temperature { get; set; }
    public int max_tokens { get; set; }
}