using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManager.Models;
public class UserTasks
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Title { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
    public DateOnly Deadline { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = false;

    [NotMapped]
    public string TruncatedDescription => Description.Length > 150 ? Description.Substring(0, 100) + "..." : Description;

    // [ForeignKey("UserId")]
    // public User User { get; set; }

    // public int UserId { get; set; }
}