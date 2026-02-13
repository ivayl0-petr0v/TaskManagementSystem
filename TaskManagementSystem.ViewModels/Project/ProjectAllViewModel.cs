namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectAllViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string DueDate { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
    }
}