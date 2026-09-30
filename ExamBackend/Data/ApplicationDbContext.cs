using ExamBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions => Set<Question>();

    public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Choices)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .Property(q => q.QuestionText)
            .IsRequired();

        modelBuilder.Entity<QuestionChoice>()
            .Property(c => c.ChoiceText)
            .IsRequired();

        modelBuilder.Entity<QuestionChoice>()
        .HasIndex(c => new
        {
            c.QuestionId,
            c.ChoiceNo
        })
        .IsUnique();
    }
}