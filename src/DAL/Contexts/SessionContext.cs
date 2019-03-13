using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using order.DomainModels;

namespace order.DAL
{

  public class SessionContext : DbContext
  {
    private readonly ILogger<SessionContext> _logger;
    public DbSet<Session> Session { get; set; }

    public SessionContext(
        DbContextOptions<SessionContext> options,
        ILogger<SessionContext> logger
    ) : base(options)
    {
      _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // modelBuilder.Properties<string>().Configure(p => p.IsMaxLength());
      foreach (var property in modelBuilder.Model.GetEntityTypes()
          .SelectMany(t => t.GetProperties())
          .Where(p => p.ClrType == typeof(string)))
      {
        property.AsProperty().Builder
            .HasMaxLength(255, ConfigurationSource.Convention);
      }

      // Other entities’ configuration ...
      modelBuilder
          .ApplyConfiguration(new OrderEntityConfiguration());
        }


    /// <summary>
    /// Override the SaveChanges so we can time the DB changes
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
      Stopwatch timer = Stopwatch.StartNew();
      var result = base.SaveChanges();
      _logger.LogInformation("EF Core database updates finished in {duration} ms", timer.ElapsedMilliseconds);
      return result;
    }

  }

  public class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
  {
    public void Configure(EntityTypeBuilder<Session> builder)
    {
      builder.HasIndex(c => c.Id).IsUnique();
    }
  }
}