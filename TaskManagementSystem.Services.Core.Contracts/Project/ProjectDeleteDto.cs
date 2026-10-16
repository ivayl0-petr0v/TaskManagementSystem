namespace TaskManagementSystem.Services.Core.Contracts.Project
{
    public class ProjectDeleteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
