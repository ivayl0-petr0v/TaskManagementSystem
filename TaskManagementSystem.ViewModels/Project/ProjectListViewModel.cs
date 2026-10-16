namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectListViewModel
    {
        public IEnumerable<ProjectListItemViewModel> MyProjects { get; set; } = [];

        public IEnumerable<ProjectListItemViewModel> OtherProjects { get; set; } = [];
    }
}
