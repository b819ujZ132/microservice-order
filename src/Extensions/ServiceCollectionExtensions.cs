using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using order.DAL;
using order.Services;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace order.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        var version = Assembly.GetEntryAssembly()
          .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
          .InformationalVersion;
        c.SwaggerDoc($"v{version}", new Info
        {
          Version = $"v{version}",
          Title = "Order",
          Description = "An example order API",
          License = new License
          {
            Name = "MIT",
            Url = "https://opensource.org/licenses/MIT"
          }
        });

        c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Swagger.xml"));
      });
    }

    public static void AddCompression(this IServiceCollection services)
    {
      services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
      services.AddResponseCompression();
    }

    public static void AddDependencies(this IServiceCollection services)
    {
      services
        
        // Session
        .AddScoped<SessionService>()
        .AddScoped<ISessionRepository, SessionRepository>()

        // Order
        .AddScoped<OrderService>()
        .AddScoped<IOrderRepository, OrderRepository>()

        // Unit of Work
        .AddScoped<IUnitOfWork, UnitOfWork>();
    }
  }
}