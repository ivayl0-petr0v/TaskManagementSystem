namespace TaskManagementSystem.Services.Core.Contracts.Project
{
    public class UpdateProjectRequest : CreateProjectRequest
    {
        public int Id { get; set; }
    }
}