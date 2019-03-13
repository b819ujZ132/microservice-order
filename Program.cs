using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace order
{
  public class Program
  {
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
      .AddEnvironmentVariables()
      .Build();

    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

      try
      {
        Log.Information("Here we go!");

        BuildWebHost(args).Run();
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseConfiguration(Configuration)
        .UseSerilog()
        .Build();
  }
}
