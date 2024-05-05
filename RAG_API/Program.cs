using EmbeddingService.Interfaces;
using EmbeddingService.Services.Embedding;
using EmbeddingService.Services.Extension;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using RAG_API.Interfaces;
using RAG_API.Service;
namespace RAG_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks();
            builder.Services.AddDependency();
            // Add Semantic Kernel
            //builder.Services.AddSingleton<IKernelBuilder>(serviceProvider => Kernel.CreateBuilder());

 
#pragma warning disable SKEXP0001
            builder.Services.AddSingleton<ISemanticTextMemory>(svcProv =>
                new MemoryBuilder()
                    .WithTextEmbeddingGeneration(ServiceExtension.EmbeddingGenerator(builder.Configuration))
                    .WithMemoryStore(ServiceExtension.PersistentMemory(builder.Configuration))
                    .Build());
#pragma warning restore SKEXP0001



            var k = Kernel.CreateBuilder();
            k.Build().Services.GetService<IChatCompletionService>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

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
            app.MapGet("/", async (HttpContext context) =>
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
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddTransient<IDocumentHandler, DocumentHandler>();
            services.AddTransient<IEmbeddingGenerator, EmbeddingGenerator > ();
            return services;
        }
    }
}
