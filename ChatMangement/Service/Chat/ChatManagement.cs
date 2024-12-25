using Azure.AI.OpenAI;
using ChatManagement.Interface;
using ChatManagement.Model.Chat;
using ChatService.Interface;
using ChatService.Model;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace ChatManagement.Service.Chat
{
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0050
    public class ChatManagement : IChatManagement
    {
        private readonly ISemanticTextMemory _skMemory;
        private readonly IChatCompletionService _chatService;
        private readonly PromptExecutionSettings _promptSettings;

        public ChatManagement(IOptions<ChatModel> model,ISemanticTextMemory skMemory,IChatService chat)

        {
            _skMemory = skMemory;
            _chatService = chat.CreateChatService(model.Value);
            _promptSettings = chat.CreatePromptExecutionSetting(model.Value);
        }

        public async Task<ChatOutput> ChatHandling(ChatInput value)
        {
            ChatHistory history = new();

            foreach (var chat in value.messages )
            {
                if (chat.role == "user") 
                    history.AddSystemMessage(chat.content);
                else if (chat.role == "system")
                    history.AddSystemMessage(chat.content);
                else history.AddAssistantMessage(chat.content);
            }

            
            Kernel kernel = Kernel.CreateBuilder().Build();
             
            kernel.ImportPluginFromObject(new TextMemoryPlugin(_skMemory),"TextMemoryPlugin");

            
            var result = await _chatService.GetChatMessageContentsAsync(history,_promptSettings ,kernel);

            var final = result.Last();

            Message msMessage = new Message() 
                { role = final.Role.Label, content = final.Content };
            var TotalToken = final.Metadata?["Usage"] as CompletionsUsage;
            ChatOutput output = new ChatOutput()
            {
                OutputMessage = msMessage,TotalToken =TotalToken.TotalTokens

            };
            return output;
        }

        
    }
}
