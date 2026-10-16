using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.GloballyCommon.ValidationConstants;

namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectCreateViewModel
    {
        [Required]
        [MinLength(ProjectTitleMinLength)]
        [MaxLength(ProjectTitleMaxLength)]
        public string Title { get; set; } = null!;

        [MinLength(ProjectDescriptionMinLength)]
        [MaxLength(ProjectDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<LookupViewModel> Statuses { get; set; } = [];

        public IEnumerable<LookupViewModel> Categories { get; set; } = [];
    }
}
