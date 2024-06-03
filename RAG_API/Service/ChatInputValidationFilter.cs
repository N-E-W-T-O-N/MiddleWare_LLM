
using ChatManagement.Model.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RAG_API.Service
{
    public class ChatInputValidationFilter : Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.ActionArguments.ContainsKey("value") || context.ActionArguments["value"] == null)
            {
                context.Result = new BadRequestObjectResult("Input cannot be null.");
                return;
            }

            var chatInput = context.ActionArguments["value"] as ChatInput;

            if (chatInput == null)
            {
                context.Result = new BadRequestObjectResult("Input cannot be null.");
                return;
            }

            // Validate the role type
            foreach (var message in chatInput.messages)
            {
                if (message.role != "system" && message.role != "user" && message.role != "assistant")
                {
                    context.Result = new BadRequestObjectResult("Invalid Role type");
                    return;
                }
            }

            // Validate the temperature
            if (chatInput.temperature > 1)
            {
                context.Result = new BadRequestObjectResult("Temperature must be less than or equal to 1");
                return;
            }

            await next();
        }
    }

}
