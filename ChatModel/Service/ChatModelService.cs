using ChatService.Interface;
using ChatService.Model;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace ChatService.Service
{
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0070
    public class ChatModelService : IChatService
    {

        public PromptExecutionSettings CreatePromptExecutionSetting(ChatModel chat)
        {

            PromptExecutionSettings setting = chat.Type switch

            {
                ModelServiceType.AzureOpenAI => new AzureOpenAIPromptExecutionSettings(),

                ModelServiceType.OpenAI => new OpenAIPromptExecutionSettings(),

                ModelServiceType.HF => new HuggingFacePromptExecutionSettings(),

                ModelServiceType.Ollama => new OllamaPromptExecutionSettings(),

                _ => new PromptExecutionSettings()
            };
            return setting;
        }

        public IChatCompletionService CreateChatService(ChatModel model)
        {
            if (model.Type == ModelServiceType.AzureOpenAI)

                return new AzureOpenAIChatCompletionService(model.AzureOpenAI.Deployment, model.AzureOpenAI.EndPoint,
                    model.AzureOpenAI.APIKey);

            else if (model.Type == ModelServiceType.OpenAI)
                return new OpenAIChatCompletionService(model.OpenAI.Deployment,
                    model.OpenAI.APIKey);

            else if (model.Type == ModelServiceType.HF)
                return new HuggingFaceChatCompletionService(model.HuggingFace.ModelID);

            throw new ArgumentException("ChatCompletionService is not Implemented for the following TYPE");
        }
    }
}