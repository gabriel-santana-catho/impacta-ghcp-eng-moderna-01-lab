using Microsoft.EntityFrameworkCore;

namespace TrainingCatalog.Infrastructure;

public sealed class TrainingCatalogDbContext(DbContextOptions<TrainingCatalogDbContext> options) : DbContext(options)
{
    public DbSet<TrainingEntity> Trainings => Set<TrainingEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var training = modelBuilder.Entity<TrainingEntity>();

        training.ToTable("Trainings");
        training.HasKey(entity => entity.Id);
        training.HasIndex(entity => entity.StartDate).IsUnique();
        training.Property(entity => entity.Title).IsRequired();
        training.Property(entity => entity.Description).IsRequired();
    }
}