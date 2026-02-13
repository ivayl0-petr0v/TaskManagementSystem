using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.ViewModels.Project;
using TaskManagementSystem.Web.Data;
using static TaskManagementSystem.GCommon.ApplicationConstants;

namespace TaskManagementSystem.Web.Controllers
{
    public class ProjectController : Controller
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
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

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
            CreateProjectInputModel createProjectInputModel = new CreateProjectInputModel
            {
                Statuses = await GetSelectProjectStatuses(),
                Categories = await GetSelectProjectCategories(),
            };

            return View(createProjectInputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProjectInputModel inputModel)
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
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
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

        public async Task<IActionResult> Details(int id)
        {
            //ProjectAllViewModel? projectDetailsViewModel = await dbContext.Projects
            //    .Include(p => p.Category)
            //    .Include(p => p.Status)
            //    .AsNoTracking()
            //    .Where(p => p.Id == id)
            //    .Select(p => new ProjectAllViewModel
            //    {
            //        Id = p.Id,
            //        Title = p.Title,
            //        Description = p.Description,
            //        Category = p.Category.Name,
            //        UserFullName = $"{p.User.FirstName} {p.User.LastName}",
            //        DueDate = p.DueDateTime.ToString(DateFormat, CultureInfo.InvariantCulture),
            //        Status = p.Status.Name
            //    })
            //    .FirstOrDefaultAsync();
            //if (projectDetailsViewModel == null)
            //{
            //    return NotFound();
            //}
            //return View(projectDetailsViewModel);
            return Ok();
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