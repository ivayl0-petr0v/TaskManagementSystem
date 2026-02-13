namespace TaskManagementSystem.ViewModels.Project
{
    public class ProjectListViewModel
    {
        public IEnumerable<ProjectAllViewModel> MyProjects { get; set; }
            = new List<ProjectAllViewModel>();

        public IEnumerable<ProjectAllViewModel> OtherProjects { get; set; }
            = new List<ProjectAllViewModel>();
    }
}
