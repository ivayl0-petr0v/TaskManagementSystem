using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Services.Core.Contracts.Project;
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

            IEnumerable<ProjectListItemViewModel> allProjects = (await projectService
                .GetAllProjectsAsync())
                .Select(MapProjectListItem);

            ProjectListViewModel model = new()
            {
                MyProjects = allProjects.Where(p => p.UserId == currentUserId),
                OtherProjects = allProjects.Where(p => p.UserId != currentUserId)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProjectCreateDataDto createData = await projectService.GetProjectCreateDataAsync();
            ProjectCreateViewModel model = MapProjectCreateViewModel(createData);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ProjectCreateDataDto reloadModel = await projectService.GetProjectCreateDataAsync();
                inputModel.Statuses = reloadModel.Statuses.Select(MapLookup);
                inputModel.Categories = reloadModel.Categories.Select(MapLookup);
                return View(inputModel);
            }

            try
            {
                CreateProjectRequest request = new()
                {
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    DueDate = inputModel.DueDate,
                    StatusId = inputModel.StatusId,
                    CategoryId = inputModel.CategoryId
                };

                await projectService.CreateProjectAsync(request, GetCurrentUserId()!);
                logger.LogInformation("Project created successfully.");
                TempData["SuccessMessage"] = "Project created successfully.";
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "Invalid Category or Status.");

                ProjectCreateDataDto reloadModel = await projectService.GetProjectCreateDataAsync();
                inputModel.Statuses = reloadModel.Statuses.Select(MapLookup);
                inputModel.Categories = reloadModel.Categories.Select(MapLookup);
                return View(inputModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating project.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred.");

                ProjectCreateDataDto reloadModel = await projectService.GetProjectCreateDataAsync();
                inputModel.Statuses = reloadModel.Statuses.Select(MapLookup);
                inputModel.Categories = reloadModel.Categories.Select(MapLookup);
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProjectDetailsDto? dto = await projectService.GetProjectDetailsByIdAsync(id, GetCurrentUserId()!);

            if (dto == null)
            {
                TempData["ErrorMessage"] = "Project not found.";
                return RedirectToAction(nameof(All));
            }

            ProjectDetailsViewModel model = MapProjectDetails(dto);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                await projectService.CompleteProjectAsync(id, GetCurrentUserId()!);

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
            UpdateProjectRequest? editRequest = await projectService.GetProjectForEditAsync(id, GetCurrentUserId()!);

            if (editRequest == null)
            {
                TempData["ErrorMessage"] = "Project not found or you are not authorized.";
                return RedirectToAction(nameof(All));
            }

            ProjectCreateDataDto createData = await projectService.GetProjectCreateDataAsync();
            ProjectEditViewModel model = new()
            {
                Id = editRequest.Id,
                Title = editRequest.Title,
                Description = editRequest.Description,
                DueDate = editRequest.DueDate,
                StatusId = editRequest.StatusId,
                CategoryId = editRequest.CategoryId,
                Statuses = createData.Statuses.Select(MapLookup),
                Categories = createData.Categories.Select(MapLookup)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, ProjectEditViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ProjectCreateDataDto reloadModel = await projectService.GetProjectCreateDataAsync();

                inputModel.Statuses = reloadModel.Statuses.Select(MapLookup);
                inputModel.Categories = reloadModel.Categories.Select(MapLookup);
                return View(inputModel);
            }

            try
            {
                UpdateProjectRequest request = new()
                {
                    Id = inputModel.Id,
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    DueDate = inputModel.DueDate,
                    StatusId = inputModel.StatusId,
                    CategoryId = inputModel.CategoryId
                };

                await projectService.EditProjectAsync(id, request, GetCurrentUserId()!);

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

                ProjectCreateDataDto reloadModel = await projectService.GetProjectCreateDataAsync();
                inputModel.Statuses = reloadModel.Statuses.Select(MapLookup);
                inputModel.Categories = reloadModel.Categories.Select(MapLookup);
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProjectDeleteDto? dto = await projectService.GetProjectForDeleteAsync(id, GetCurrentUserId()!);

            if (dto == null)
            {
                TempData["ErrorMessage"] = "Project not found or access denied.";
                return RedirectToAction(nameof(All));
            }

            ProjectDeleteViewModel model = new()
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await projectService.DeleteProjectAsync(id, GetCurrentUserId()!);

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

        private static ProjectCreateViewModel MapProjectCreateViewModel(ProjectCreateDataDto createData)
        {
            return new ProjectCreateViewModel
            {
                DueDate = DateTime.Today,
                Statuses = createData.Statuses.Select(MapLookup),
                Categories = createData.Categories.Select(MapLookup)
            };
        }

        private static LookupViewModel MapLookup(LookupDto lookupDto)
        {
            return new LookupViewModel
            {
                Id = lookupDto.Id,
                Name = lookupDto.Name
            };
        }

        private static ProjectListItemViewModel MapProjectListItem(ProjectListItemDto dto)
        {
            return new ProjectListItemViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = dto.Status,
                Category = dto.Category,
                UserId = dto.UserId,
                UserFullName = dto.UserFullName
            };
        }

        private static ProjectDetailsViewModel MapProjectDetails(ProjectDetailsDto dto)
        {
            return new ProjectDetailsViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = dto.Status,
                Category = dto.Category,
                UserId = dto.UserId,
                UserFullName = dto.UserFullName,
                IsOwner = dto.IsOwner,
                IsCompleted = dto.IsCompleted
            };
        }
    }
}
