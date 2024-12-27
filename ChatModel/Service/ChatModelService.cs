using ChatService.Interface;
using ChatService.Model;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Memory;

namespace ChatService.Service
{
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0070
    public class ChatModelService : IChatService
    {
        private Dictionary<string, object> _extensionDict = new ()
        {
            //[Microsoft.SemanticKernel.Plugins.Memory.TextMemoryPlugin.InputParam] = "Ask: my family is from?",
            [TextMemoryPlugin.CollectionParam] = "RAGFILECOLLECTION",
            [TextMemoryPlugin.LimitParam] = "2",
            [TextMemoryPlugin.RelevanceParam] = "0.79",
        };
        public PromptExecutionSettings CreatePromptExecutionSetting(ChatModel chat)
        {
            


            PromptExecutionSettings setting = chat.Type switch

            {
                ModelServiceType.AzureOpenAI => new AzureOpenAIPromptExecutionSettings()
                {
                    Temperature = chat.AzureOpenAI.Temperature,
                    TopP = chat.AzureOpenAI.TopP,
                    PresencePenalty = chat.AzureOpenAI.PresencePenality,
                    MaxTokens = chat.AzureOpenAI.MaxTokens,
                    FrequencyPenalty = chat.AzureOpenAI.FrequencyPenalty,
                    StopSequences = chat.AzureOpenAI.Stop,
                    ExtensionData = _extensionDict
                },

                ModelServiceType.OpenAI => new OpenAIPromptExecutionSettings()
                { ExtensionData = _extensionDict },

                ModelServiceType.HF => new HuggingFacePromptExecutionSettings() 
                    { MaxTokens = 100, ExtensionData = _extensionDict },

                ModelServiceType.Ollama => new OllamaPromptExecutionSettings()
                    {
                        Temperature =(float?) chat.Ollama.Temperature,
                        Stop=chat.Ollama.Stop?.ToList(),
                        ExtensionData = _extensionDict
                    },

                _ => new PromptExecutionSettings()
            };

            setting.FunctionChoiceBehavior = FunctionChoiceBehavior.Auto();

            return setting;
        }

        public IChatCompletionService CreateChatService(ChatModel model)
        {
            if (model.Type == ModelServiceType.AzureOpenAI)
                return new AzureOpenAIChatCompletionService(model.AzureOpenAI.Deployment, model.AzureOpenAI.EndPoint, model.AzureOpenAI.APIKey);

            if (model.Type == ModelServiceType.OpenAI)
                return new OpenAIChatCompletionService(model.OpenAI.Deployment,
                    model.OpenAI.APIKey);

            if (model.Type == ModelServiceType.HF)
                return new HuggingFaceChatCompletionService(model.HuggingFace.ModelID);

            if (model.Type == ModelServiceType.Ollama)
                return new OllamaChatCompletionService(model.Ollama.Model, new Uri(model.Ollama.URL));

            throw new ArgumentException("ChatCompletionService is not Implemented for the following TYPE");
        }
    }
}