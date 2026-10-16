namespace TaskManagementSystem.Services.Core.Contracts.Project
{
    public class CreateProjectRequest
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public int StatusId { get; set; }

        public int CategoryId { get; set; }
    }
}
