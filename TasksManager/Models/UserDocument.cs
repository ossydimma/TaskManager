using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManager.Models
{
    public class UserDocument 
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string Title { get; set;} = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public string Content { get; set;} = string.Empty;

        [NotMapped]
        public bool IsHovered { get; set; } = false;

        [NotMapped]
        public string FormattedDate => CurrentDate.ToString("dd MMMM yyyy");

        [NotMapped]
        public string HalvedContent { 
            get
            {
               int MaxLength = 200;
                if (Content.Length > MaxLength)
                {
                    return Content.Substring(0, MaxLength) + "...";
                } 
                return Content;
            }
        }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public Guid UserId { get; set; }
        
    }

}