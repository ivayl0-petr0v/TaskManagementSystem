namespace TaskManagementSystem.ViewModels.Project;

using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.GCommon.ValidationConstants;

public class CreateProjectInputModel
{
    //Input Data
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

    //[Required]
    //public string UserId { get; set; } = null!;

    //Output Data
    public IEnumerable<SelectProjectStatusViewModel> Statuses { get; set; }
        = new List<SelectProjectStatusViewModel>();

    public IEnumerable<SelectProjectCategoryViewModel> Categories { get; set; }
        = new List<SelectProjectCategoryViewModel>();

    //public ICollection<ProjectUsersViewModel> Users { get; set; }
    //    = new HashSet<ProjectUsersViewModel>();

}
