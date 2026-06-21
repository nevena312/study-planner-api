using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Data;

public class StudyPlannerDbContext : DbContext
{
    public StudyPlannerDbContext(DbContextOptions<StudyPlannerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<StudyTask> StudyTasks { get; set; }
    public DbSet<StudyPlan> StudyPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Subjects)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.StudyPlans)
            .WithOne(sp => sp.User)
            .HasForeignKey(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Exams)
            .WithOne(e => e.Subject)
            .HasForeignKey(e => e.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.StudyTasks)
            .WithOne(t => t.Subject)
            .HasForeignKey(t => t.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudyPlan>()
            .HasMany(sp => sp.StudyTasks)
            .WithOne(t => t.StudyPlan)
            .HasForeignKey(t => t.StudyPlanId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}