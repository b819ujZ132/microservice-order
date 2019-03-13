using Amazon.SecretsManager;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using order.Services;
using order.DAL;


namespace order
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IHostingEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IHostingEnvironment Environment { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvcCore()
          .AddApiExplorer();

      // Swagger documentation
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
          {
            Version = "v1",
            Title = "Order",
            Description = "An example order API",
            License = new License {
              Name = "MIT",
              Url = "https://opensource.org/licenses/MIT"
            }          
          });

        c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Swagger.xml"));
      });

      // Compression
      services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
      services.AddResponseCompression();

      // CORS
      services.AddCors();

      // Databases
      using (var client = new AmazonSecretsManagerClient())
      {
        // TODO: Add client library and use AWS KMS to fetch secret keys
      }

      // AutoMapper
      Mapper.Initialize(cfg =>
      {
        // Empty for now
      });

      // Dependency Injection
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

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

      // TODO: Add logger middleware for production environment

      app.UseHttpsRedirection();

      app.UseMvc();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseSwagger();
      app.UseSwaggerUI();

      Configure(app, env);
    }
  }
}