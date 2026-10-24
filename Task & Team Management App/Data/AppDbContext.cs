using Microsoft.EntityFrameworkCore;
using TaskTeamManagementApp.Models;

namespace TaskTeamManagementApp.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(project => project.Id);
            entity.Property(project => project.Name).HasMaxLength(120).IsRequired();
            entity.Property(project => project.Description).HasMaxLength(1000);
            entity.Property(project => project.CreatedAtUtc).IsRequired();
        });
    }
}
