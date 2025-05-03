using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TasksManager.Models
{
    public class User : IdentityUser
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FullName { get; set; } = string.Empty;

        // [Required]
        // [StringLength(50, MinimumLength = 3)]
        // public string Email { get; set; } = string.Empty;

        // public byte[] PasswordHash { get; set; } = null!;
        // public byte[] PasswordSalt { get; set; } = null!;

        // public string? Token { get; set; }

        [JsonIgnore]
        public virtual List<UserDocument> UserDocument { get; set; } = [];
        [JsonIgnore]
        public virtual List<UserTasks> UserTasks { get; set; } = [];
    }
}