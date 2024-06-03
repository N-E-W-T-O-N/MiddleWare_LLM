namespace ChatManagement.Model.Chat;

// { "role": "system", "content": "Always answer in rhymes." },
// { "role": "user", "content": "Introduce yourself." }
public class Message
{
    public static string User = "user";
    public static string system = "system";
    public string role { get; set; }
    public string content { get; set; }
}