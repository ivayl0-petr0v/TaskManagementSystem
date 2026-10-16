namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectDetailsViewModel : ProjectListItemViewModel
    {
        public bool IsOwner { get; set; }

        public bool IsCompleted { get; set; }
    }
}
