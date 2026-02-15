using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TaskManagementSystem.GCommon.ValidationConstants;

namespace TaskManagementSystem.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProjectTitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(ProjectDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDateTime { get; set; }

        [Required]
        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}