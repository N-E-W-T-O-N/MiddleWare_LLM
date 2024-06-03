using Azure.AI.OpenAI;
using ChatManagement.Interface;
using ChatManagement.Model.Chat;
using ChatManagement.Model.ChatModel;
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

        public ChatManagement(IOptions<ChatModel> model,ISemanticTextMemory skMemory)

        {
            _skMemory = skMemory;
            _chatService = CreateChatService(model.Value);
            _promptSettings = CreatePromptExecutionSetting(model.Value);
        }

        private PromptExecutionSettings CreatePromptExecutionSetting(ChatModel chat)
        {

            PromptExecutionSettings setting = chat.Type switch

            {
                ModelServiceType.AzureOpenAI => new OpenAIPromptExecutionSettings()
                    ,
                //ModelServiceType.H = new HuggingFacePromptExecutionSettings(),
                _ => new  PromptExecutionSettings()
            };
            return setting;
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

        private IChatCompletionService CreateChatService(ChatModel model)
        {
            if (model.Type == ModelServiceType.AzureOpenAI)
                return new AzureOpenAIChatCompletionService(model.AzureOpenAI.Deployment, model.AzureOpenAI.EndPoint,
                    model.AzureOpenAI.APIKey);
            else return new OpenAIChatCompletionService(model.OpenAI.Deployment,
                    model.OpenAI.APIKey);
        }
    }
}
