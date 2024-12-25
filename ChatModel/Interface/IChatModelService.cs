using ChatService.Model;
using Microsoft.SemanticKernel.ChatCompletion;

namespace ChatService.Interface;

public interface IChatModelService
{
    public IChatCompletionService CreateChatService(ChatModel model);
}