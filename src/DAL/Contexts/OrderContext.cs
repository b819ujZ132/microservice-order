using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using order.DomainModels;

namespace order.DAL
{
  public class OrderContext : DbContext
  {
    private readonly ILogger<OrderContext> _logger;
    public DbSet<Order> Order { get; set; }
    public OrderContext(
        DbContextOptions<OrderContext> options,
        ILogger<OrderContext> logger
    ) : base(options)
    {
      _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var property in modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(string)))
      {
        property.AsProperty().Builder
          .HasMaxLength(255, ConfigurationSource.Convention);
      }

      // Other entities’ configuration ...
      modelBuilder
        .ApplyConfiguration(new OrderEntityConfiguration())
        .ApplyConfiguration(new OrderStatusEntityConfiguration());
    }


    /// <summary>
    /// Time DB changes
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

  class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder
        .HasKey(b => b.OrderId);

      builder.HasIndex(o => o.SessionId).IsUnique();
      builder.HasIndex(o => new { o.OrderId, o.SessionId }).IsUnique();

      builder
        .Property(p => p.DateAdded).HasColumnType("Date")
        .HasDefaultValueSql("CURRENT_DATE");

      builder
        .Property(p => p.DateTimeAdded).HasColumnType("DateTime")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        ;
    }
  }

  class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatus>
  {
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
      builder
        .HasKey(b => b.StatusId);

      builder.HasIndex(o => o.StatusId).IsUnique();
      builder.HasIndex(o => new { o.OrderId, o.StatusId }).IsUnique();

      builder
        .Property(p => (int)p.StatusEnum).HasColumnType("tinyint");

      builder
        .Property(p => p.DateAdded).HasColumnType("Date")
        .HasDefaultValueSql("CURRENT_DATE");

      builder
        .Property(p => p.DateTimeAdded).HasColumnType("DateTime")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        ;
    }
  }
}