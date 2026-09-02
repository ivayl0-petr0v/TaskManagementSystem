namespace TaskManagementSystem.ViewModels.Project;

using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.GCommon.ValidationConstants;

public class ProjectInputModel
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

    //Output Data
    public IEnumerable<SelectProjectStatusViewModel> Statuses { get; set; }
        = new List<SelectProjectStatusViewModel>();

    public IEnumerable<SelectProjectCategoryViewModel> Categories { get; set; }
        = new List<SelectProjectCategoryViewModel>();
}
