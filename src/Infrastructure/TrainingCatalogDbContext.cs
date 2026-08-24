using Microsoft.EntityFrameworkCore;

namespace TrainingCatalog.Infrastructure;

public sealed class TrainingCatalogDbContext(DbContextOptions<TrainingCatalogDbContext> options) : DbContext(options)
{
    public DbSet<TrainingEntity> Trainings => Set<TrainingEntity>();

    public DbSet<AttendeeEntity> Attendees => Set<AttendeeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var training = modelBuilder.Entity<TrainingEntity>();

        training.ToTable("Trainings");
        training.HasKey(entity => entity.Id);
        training.HasIndex(entity => entity.StartDate).IsUnique();
        training.Property(entity => entity.Title).IsRequired();
        training.Property(entity => entity.Description).IsRequired();

        var attendee = modelBuilder.Entity<AttendeeEntity>();

        attendee.ToTable("Attendees");
        attendee.HasKey(entity => entity.Id);
        attendee.HasIndex(entity => new { entity.TrainingId, entity.EmailNormalized }).IsUnique();
        attendee.Property(entity => entity.FirstName).IsRequired();
        attendee.Property(entity => entity.LastName).IsRequired();
        attendee.Property(entity => entity.Email).IsRequired();
        attendee.Property(entity => entity.EmailNormalized).IsRequired();
        attendee.HasOne(entity => entity.Training)
            .WithMany(entity => entity.Attendees)
            .HasForeignKey(entity => entity.TrainingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}