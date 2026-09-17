using Microsoft.EntityFrameworkCore;

namespace CreditProducts.Infrastructure.Persistence;

public class CreditProductDbContext : DbContext
{
    public CreditProductDbContext(DbContextOptions<CreditProductDbContext> options)
        : base(options)
    {

    }

    public DbSet<Domain.Entities.CreditProduct> CreditProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.CreditProduct>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            e.HasIndex(x => x.Code)
                .IsUnique()
                .HasDatabaseName("UQ_Code");
            
            e.Property(x => x.MinAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            e.Property(x => x.MaxAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            e.Property(x => x.MinTermMonths)
                .IsRequired();
            
            e.Property(x => x.MaxTermMonths)
                .IsRequired();
            
            e.Property(x => x.MaxInstallmentIncomeRatio)
                .HasColumnType("decimal(3,2)")
                .IsRequired();
            
            e.Property(x => x.IsActive)
                .IsRequired().HasDefaultValue(true);
            
            e.Property(x => x.CreatedAtUtc)
                .IsRequired();
            
            
        });
    }
}