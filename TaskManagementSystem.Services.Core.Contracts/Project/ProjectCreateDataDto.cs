namespace TaskManagementSystem.Services.Core.Contracts.Project;

public class ProjectCreateDataDto
{
    public IEnumerable<LookupDto> Statuses { get; set; } = [];

    public IEnumerable<LookupDto> Categories { get; set; } = [];
}
