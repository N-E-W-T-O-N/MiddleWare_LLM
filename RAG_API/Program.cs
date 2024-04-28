
using EmbeddingService;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Embeddings;

using Microsoft.SemanticKernel.Connectors.HuggingFace;
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
            builder.Services.AddSingleton<IKernelBuilder>(serviceProvider => Kernel.CreateBuilder());
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0020
            //builder.Services.AddTransient<IMemoryStore>(sp=>new MemoryBuilder().Build());
            //builder.Services.AddTransient<ITextEmbeddingGenerationService>(sp => sp.PersistentMemory(builder.Configuration));
            builder.Services.AddSingleton<ISemanticTextMemory>(svcProv =>
                new MemoryBuilder().WithTextEmbeddingGeneration(MethodExtension.EmbeddingGenerator(builder.Configuration)).WithMemoryStore(MethodExtension.PersistentMemory(builder.Configuration)).Build());

            


            var k = Kernel.CreateBuilder();
                k.Build().Services.GetService<IChatCompletionService>()
           
            //builder.Services.AddTransient<ITextEmbeddingGenerationService>(src=>src.)
            var t = new MemoryBuilder().WithTextEmbeddingGeneration(MethodExtension.EmbeddingGenerator(builder.Configuration))
                .WithMemoryStore(new QdrantMemoryStore("", 123)).Build(); 
#pragma warning restore SKEXP0020
#pragma warning restore SKEXP0001
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
        /// Define My Own Dependency Here
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddTransient<IDocumentHandler, DocumentHandler>();
            return services;
        }
#pragma warning disable SKEXP0001


        public static IMemoryStore PersistentMemory(this IServiceProvider provider,
            ConfigurationManager configuration)

        {
        }
#pragma warning disable SKEXP0070
        public static ITextEmbeddingGenerationService EmbeddingGenerator( ConfigurationManager configuration)
        {

        EmebeddingModelType type = new();
            var section = configuration.GetSection(EmebeddingModelType.PropertyName);
            section.Bind(type);


            ITextEmbeddingGenerationService ser = type.EmbeddingType switch
            {
                EmbeddingType.AzureOpenAI => new AzureOpenAITextEmbeddingGenerationService(, type.AzureOpenAI.Endpoint,
                    type.AzureOpenAI.APIKey),
                EmbeddingType.OpenAI => new OpenAITextEmbeddingGenerationService("", ""),

                "Hugging" => new HuggingFaceTextEmbeddingGenerationService("", new Uri("")),
                //EmbeddingType.Ollama => new TextEmbedd,
                _ => throw new ArgumentException("NOT ALL VALES ARE PASSED")

            };


            //      var section = configuration.GetSection(EmebeddingModelType.PropertyName);




            return ser;
        }
    }
}
