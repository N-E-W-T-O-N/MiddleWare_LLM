using System.Threading.RateLimiting;
using ChatManagement.Interface;
using ChatService.Interface;
using ChatService.Model;
using EmbeddingService.Interfaces;
using EmbeddingService.Services.Embedding;
using LLM_API.Interfaces;
using LLM_API.Service;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.RateLimiting;
using SemanticKernelFactory;

namespace LLM_API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var policy = "concurrent";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks();
            builder.Services.AddDependency();

#pragma warning disable SKEXP0001
            builder.Services.AddSingleton
                (svcProv => SkFactory.CreateSkMemory(builder.Configuration));
#pragma warning restore SKEXP0001


            builder.Services.Configure<ChatModel>(builder.Configuration.GetSection("ChatModel"));

            // Apply filter to all API ENDPOINT 
            builder.Services.AddControllers();
            //builder.Services.AddControllers(config => config.Filters.Add<ChatInputValidationFilter>());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            builder.Services.AddRateLimiter(srv =>
            srv.AddConcurrencyLimiter(policy, config =>
            {
                config.PermitLimit = 4;
                config.QueueLimit = 10;
                config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            })

            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHealthChecks("/healthz");
            //Test API is working or not 
            app.MapGet("/", async (context) =>
            {
                await context.Response.WriteAsync("API WORKING");
            }
            );

            // Get token
            app.MapGet("antiforgery/token", (HttpContext context, IAntiforgery forgeryService) =>
            {
                var tokens = forgeryService.GetAndStoreTokens(context);
                var xsrfToken = tokens.RequestToken!;
                return TypedResults.Content(xsrfToken, "text/plain");
            });

            app.Run();
        }

    }
    public static class MethodExtension
    {
        /// <summary>
        /// Define your Own Dependency Here
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {

            // Add services to the container.
            services.AddTransient<IChatHandler, ChatHandler>();
            services.AddTransient<IDocumentHandler, DocumentHandler>();
            services.AddTransient<IEmbeddingGenerator, EmbeddingGenerator>();
            services.AddTransient<IChatService, ChatService.Service.ChatModelService>();
            services.AddTransient<IChatManagement, ChatManagement.Service.Chat.ChatManagement>();
            return services;
        }
    }
}
