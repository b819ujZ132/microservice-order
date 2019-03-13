using Amazon.SecretsManager;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using order.Extensions;

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

      // Swagger
      services.AddSwagger();

      // Compression
      services.AddCompression();

      // CORS
      services.AddCors();

      // Databases
      /*
      using (var client = new AmazonSecretsManagerClient())
      {
        // TODO: Add client library and use AWS KMS to fetch secret keys
      }
      */

      // AutoMapper
      Mapper.Initialize(cfg =>
      {
        // Empty for now
      });

      // Filters
      services.AddFilters();

      // Dependency Injection
      services.AddDependencies();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
    {
      app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

      // TODO: Add logger middleware for production environment
      // app.UseMiddleware();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseResponseCompression();

      app.UseHttpsRedirection();

      app.UseMvc();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
    {
      app.UseSwagger();
      app.UseSwaggerUI();

      Configure(app, env, factory);
    }
  }
}