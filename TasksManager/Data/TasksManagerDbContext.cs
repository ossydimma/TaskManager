
using Microsoft.EntityFrameworkCore;
using TasksManager.Models;

namespace TasksManager.Data;
public class TasksManagerDbContext : DbContext
{
    public TasksManagerDbContext(DbContextOptions<TasksManagerDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserTasks> UserTasks { get; set; } = null!;
    public DbSet<UserDocument> UserDocuments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserDocument)
            .WithOne(ud => ud.User)
            .HasForeignKey(ud => ud.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserTasks)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId);
    }
    
}