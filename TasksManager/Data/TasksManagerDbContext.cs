
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksManager.Models;

namespace TasksManager.Data;
public class TasksManagerDbContext : IdentityDbContext<User>
{
    public TasksManagerDbContext(DbContextOptions<TasksManagerDbContext> options) : base(options)
    {
    }

    public DbSet<UserTasks> UserTasks { get; set; } = default!;
    public DbSet<UserDocument> UserDocuments { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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