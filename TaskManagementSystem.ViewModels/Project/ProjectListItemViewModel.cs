namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectListItemViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string UserFullName { get; set; } = null!;
    }
}
