using Microsoft.EntityFrameworkCore;
using MarketPay.Domain.Entities;

namespace MarketPay.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Market> Markets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SupportChat> SupportChats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Indexes for better performance
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Phone);
        });

        // Market configuration
        modelBuilder.Entity<Market>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Indexes for better performance
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Name);
        });

        // Product configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ProductBarcode)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ProductPrice)
                .IsRequired()
                .HasPrecision(18, 2);

             entity.Property(e => e.ProductUnit)
                .IsRequired()
                .HasMaxLength(50);

             entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Foreign key relationship
            entity.HasOne(e => e.Market)
                .WithMany(m => m.Products)
                .HasForeignKey(e => e.MarketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for better performance
            entity.HasIndex(e => e.ProductBarcode);
            entity.HasIndex(e => e.ProductName);
            entity.HasIndex(e => e.MarketId);
        });

        // Cart configuration
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("active");

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Foreign key relationships
            entity.HasOne(e => e.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Market)
                .WithMany(m => m.Carts)
                .HasForeignKey(e => e.MarketId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better performance
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.MarketId);
            entity.HasIndex(e => e.Status);
        });

        // CartItem configuration
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Foreign key relationships
            entity.HasOne(e => e.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better performance
            entity.HasIndex(e => e.CartId);
            entity.HasIndex(e => e.ProductId);
        });

        // Payment configuration
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            entity.Property(e => e.PaidAt)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Foreign key relationships
            entity.HasOne(e => e.Cart)
                .WithOne(c => c.Payment)
                .HasForeignKey<Payment>(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better performance
            entity.HasIndex(e => e.CartId).IsUnique();
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.PaidAt);
        });

        // SupportChat configuration
        modelBuilder.Entity<SupportChat>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(e => e.Sender)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt);

            // Foreign key relationship
            entity.HasOne(e => e.User)
                .WithMany(u => u.SupportChats)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for better performance
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Sender);
            entity.HasIndex(e => e.CreatedAt);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}
