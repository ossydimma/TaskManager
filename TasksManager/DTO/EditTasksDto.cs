using System.ComponentModel.DataAnnotations;

namespace TasksManager.DTO
{
    public class EditTasksDto
    {
        [Required]
        public string? Category { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public bool Status { get; set; } = false;
    }
}