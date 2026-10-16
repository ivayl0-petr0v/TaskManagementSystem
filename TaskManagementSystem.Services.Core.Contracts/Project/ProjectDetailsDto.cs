namespace TaskManagementSystem.Services.Core.Contracts.Project
{
    public class ProjectDetailsDto : ProjectListItemDto
    {
        public bool IsOwner { get; set; }

        public bool IsCompleted { get; set; }
    }
}