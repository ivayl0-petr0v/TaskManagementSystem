using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Security.Claims;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.ViewModels.Project;
using TaskManagementSystem.Web.Data;
using static TaskManagementSystem.GCommon.ApplicationConstants;

namespace TaskManagementSystem.Web.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly TaskManagementDbContext dbContext;

        public ProjectController(TaskManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            string currentUserId = GetCurrentUserId()!;


            IEnumerable<ProjectAllViewModel> allProjects = await dbContext.Projects
                    .AsNoTracking()
                    .OrderBy(p => p.Title)
                    .ThenBy(p => p.DueDateTime)
                    .Select(p => new ProjectAllViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        DueDate = p.DueDateTime.ToString(DateFormat, CultureInfo.InvariantCulture),
                        Status = p.Status.Name,
                        Category = p.Category.Name,
                        UserFullName = p.User.UserFullName,
                        UserId = p.UserId
                    })
                    .ToListAsync();

            var model = new ProjectListViewModel
            {
                MyProjects = allProjects.Where(p => p.UserId == currentUserId),
                OtherProjects = allProjects.Where(p => p.UserId != currentUserId)
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProjectInputModel createProjectInputModel = new ProjectInputModel
            {
                Statuses = await GetSelectProjectStatuses(),
                Categories = await GetSelectProjectCategories(),
            };

            return View(createProjectInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProjectInputModel inputModel)
        {
            inputModel.Statuses = await GetSelectProjectStatuses();
            inputModel.Categories = await GetSelectProjectCategories();

            if (!ModelState.IsValid)
            {

                return View(inputModel);
            }

            bool statusExists = inputModel
                .Statuses
                .Any(s => s.Id == inputModel.StatusId);

            bool categoryExists = inputModel
                .Categories
                .Any(c => c.Id == inputModel.CategoryId);

            if (!statusExists)
            {
                ModelState.AddModelError(nameof(inputModel.StatusId), "Invalid status selected.");
                return View(inputModel);
            }

            if (!categoryExists)
            {
                ModelState.AddModelError(nameof(inputModel.CategoryId), "Invalid category selected.");
                return View(inputModel);
            }

            try
            {
                Project newProject = new Project
                {
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    DueDateTime = inputModel.DueDate,
                    StatusId = inputModel.StatusId,
                    CategoryId = inputModel.CategoryId,
                    UserId = GetCurrentUserId()!
                };

                dbContext.Projects.Add(newProject);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "An error occurred while creating the project. Please try again.");

                return View(inputModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Project? project = await GetCurrentProject(id);

            if (project == null)
            {
                return NotFound();
            }

            string? currentUserId = GetCurrentUserId();

            ProjectDetailsViewModel model = new ProjectDetailsViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Category = project.Category.Name,
                DueDate = project.DueDateTime.ToString(DateFormat, CultureInfo.InvariantCulture),
                Status = project.Status.Name,
                UserFullName = project.User.UserFullName,
                IsOwner = currentUserId?.ToLowerInvariant() == project.UserId.ToLowerInvariant(),
                IsCompleted = project.Status.Name == "Completed"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            Project? project = await GetProjectWithStatus(id);

            if (project == null)
            {
                return NotFound();
            }

            string? currentUserId = GetCurrentUserId();
            if (project.UserId.ToLowerInvariant() != currentUserId?.ToLowerInvariant())
            {
                return Unauthorized();
            }

            if (project.Status?.Name != "Completed")
            {
                Status? completedStatus = await dbContext
                    .Statuses
                    .FirstOrDefaultAsync(s => s.Name == "Completed");

                if (completedStatus != null)
                {
                    project.Status = completedStatus;

                    await dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Details), new { id = project.Id });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            Project? projectToEdit = await dbContext
                .Projects
                .SingleOrDefaultAsync(p => p.Id == id);
            if (projectToEdit == null)
            {
                return NotFound();
            }

            string? currentUserId = GetCurrentUserId();

            if (projectToEdit.UserId.ToLowerInvariant() != currentUserId?.ToLowerInvariant())
            {
                return Unauthorized();
            }

            ProjectInputModel projectInputModel = new ProjectInputModel()
            {
                Title = projectToEdit.Title,
                Description = projectToEdit.Description,
                DueDate = projectToEdit.DueDateTime,
                StatusId = projectToEdit.StatusId,
                CategoryId = projectToEdit.CategoryId,
                Statuses = await GetSelectProjectStatuses(),
                Categories = await GetSelectProjectCategories()
            };

            return View(projectInputModel);
        }

        public async Task<IActionResult> Edit([FromRoute] int id, ProjectInputModel inputModel)
        {
            inputModel.Statuses = await GetSelectProjectStatuses();
            inputModel.Categories = await GetSelectProjectCategories();

            Project? projectToEdit = await FindProjectById(id);
            if (projectToEdit == null)
            {
                return NotFound();
            }

            string? currentUserId = GetCurrentUserId();
            if (projectToEdit.UserId.ToLowerInvariant() != currentUserId?.ToLowerInvariant())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            bool statusExists = inputModel
                .Statuses
                .Any(s => s.Id == inputModel.StatusId);

            bool categoryExists = inputModel
                .Categories
                .Any(c => c.Id == inputModel.CategoryId);

            if (!statusExists)
            {
                ModelState.AddModelError(nameof(inputModel.StatusId), "Invalid status selected.");
                return View(inputModel);
            }

            if (!categoryExists)
            {
                ModelState.AddModelError(nameof(inputModel.CategoryId), "Invalid category selected.");
                return View(inputModel);
            }

            try
            {
                projectToEdit.Title = inputModel.Title;
                projectToEdit.Description = inputModel.Description;
                projectToEdit.DueDateTime = inputModel.DueDate;
                projectToEdit.StatusId = inputModel.StatusId;
                projectToEdit.CategoryId = inputModel.CategoryId;

                dbContext.Projects.Update(projectToEdit);
                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the project. Please try again.");

                return View(inputModel);
            }
        }

        private async Task<Project?> GetProjectWithStatus(int id)
        {
            return await dbContext
                .Projects
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        private async Task<Project?> FindProjectById(int id)
        {
            return await dbContext
                .Projects
                .FindAsync(id);
        }

        private async Task<Project?> GetCurrentProject(int id)
        {
            return await dbContext
                .Projects
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Status)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        private async Task<IEnumerable<SelectProjectStatusViewModel>> GetSelectProjectStatuses()
        {
            IEnumerable<SelectProjectStatusViewModel> projectStatuses = await dbContext
                .Statuses
                .AsNoTracking()
                .Select(s => new SelectProjectStatusViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToArrayAsync();

            return projectStatuses;
        }

        private async Task<IEnumerable<SelectProjectCategoryViewModel>> GetSelectProjectCategories()
        {
            IEnumerable<SelectProjectCategoryViewModel> projectCategories = await dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new SelectProjectCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArrayAsync();

            return projectCategories;
        }
    }
}