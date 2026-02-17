using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Services.Core.Interfaces;
using TaskManagementSystem.ViewModels.Project;

namespace TaskManagementSystem.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string currentUserId = GetCurrentUserId()!;

            IEnumerable<ProjectAllViewModel> allProjects = await projectService
                .GetAllProjectsAsync();

            ProjectListViewModel? model = new ProjectListViewModel
            {
                MyProjects = allProjects.Where(p => p.UserId == currentUserId),
                OtherProjects = allProjects.Where(p => p.UserId != currentUserId)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProjectInputModel model = await projectService
                .GetProjectForCreateAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ProjectInputModel? reloadModel = await projectService
                    .GetProjectForCreateAsync();

                inputModel.Statuses = reloadModel.Statuses;
                inputModel.Categories = reloadModel.Categories;
                return View(inputModel);
            }

            try
            {
                await projectService.CreateProjectAsync(inputModel, GetCurrentUserId()!);
                logger.LogInformation("Project created successfully.");
                TempData["SuccessMessage"] = "Project created successfully.";
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "Invalid Category or Status.");

                ProjectInputModel? reloadModel = await projectService
                    .GetProjectForCreateAsync();

                inputModel.Statuses = reloadModel.Statuses;
                inputModel.Categories = reloadModel.Categories;
                return View(inputModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating project.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred.");

                ProjectInputModel? reloadModel = await projectService
                    .GetProjectForCreateAsync();

                inputModel.Statuses = reloadModel.Statuses;
                inputModel.Categories = reloadModel.Categories;
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProjectDetailsViewModel? model = await projectService
                .GetProjectDetailsByIdAsync(id, GetCurrentUserId()!);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Project not found.";
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                await projectService
                    .CompleteProjectAsync(id, GetCurrentUserId()!);

                TempData["SuccessMessage"] = "Project marked as completed.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = "Project not found.";
                return RedirectToAction(nameof(All));
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "You don't have permission.";
                return RedirectToAction(nameof(All));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProjectEditInputModel? model = await projectService
                .GetProjectForEditAsync(id, GetCurrentUserId()!);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Project not found or you are not authorized.";
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, ProjectEditInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ProjectInputModel? reloadModel = await projectService
                    .GetProjectForCreateAsync();

                inputModel.Statuses = reloadModel.Statuses;
                inputModel.Categories = reloadModel.Categories;
                return View(inputModel);
            }

            try
            {
                await projectService
                    .EditProjectAsync(id, inputModel, GetCurrentUserId()!);

                TempData["SuccessMessage"] = "Project updated successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "You don't have permission.";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing project.");

                ProjectInputModel? reloadModel = await projectService
                    .GetProjectForCreateAsync();

                inputModel.Statuses = reloadModel.Statuses;
                inputModel.Categories = reloadModel.Categories;
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await projectService
                .GetProjectForDeleteAsync(id, GetCurrentUserId()!);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Project not found or access denied.";
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await projectService
                    .DeleteProjectAsync(id, GetCurrentUserId()!);

                TempData["SuccessMessage"] = "Project deleted successfully.";
                return RedirectToAction(nameof(All));
            }
            catch (UnauthorizedAccessException)
            {
                TempData["ErrorMessage"] = "Access denied.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete failed.");
                TempData["ErrorMessage"] = "Delete failed.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}