using ChatService.Model;
using Microsoft.SemanticKernel;

namespace ChatService.Interface;

public interface IPromptService
{
    /// <summary>
    /// Return Prompt Setting Object based on ChatModel type
    /// </summary>
    /// <param name="chat"></param>
    /// <returns>PromptExecutionSettings</returns>
    public PromptExecutionSettings CreatePromptExecutionSetting(ChatModel chat);
}